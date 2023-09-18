/*
 * Copyright (c) 2013 Calvin Rien
 *
 * Based on the JSON parser by Patrick van Bergen
 * http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Simplified it so that it doesn't throw exceptions
 * and can be used in Unity iPhone with maximum code stripping.
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniJSON
{
	// Example usage:
	//
	//  using UnityEngine;
	//  using System.Collections;
	//  using System.Collections.Generic;
	//  using MiniJSON;
	//
	//  public class MiniJSONTest : MonoBehaviour {
	//      void Start () {
	//          var jsonString = "{ \"array\": [1.44,2,3], " +
	//                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
	//                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
	//                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
	//                          "\"int\": 65536, " +
	//                          "\"float\": 3.1415926, " +
	//                          "\"bool\": true, " +
	//                          "\"null\": null }";
	//
	//          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
	//
	//          Debug.Log("deserialized: " + dict.GetType());
	//          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
	//          Debug.Log("dict['string']: " + (string) dict["string"]);
	//          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
	//          Debug.Log("dict['int']: " + (long) dict["int"]); // ints come out as longs
	//          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
	//
	//          var str = Json.Serialize(dict);
	//
	//          Debug.Log("serialized: " + str);
	//      }
	//  }

	/// <summary>
	/// This class encodes and decodes JSON strings.
	/// Spec. details, see http://www.json.org/
	///
	/// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
	/// All numbers are parsed to doubles.
	/// </summary>
	public static class Json
	{
		/// <summary>
		/// Parses the string json into a value
		/// </summary>
		/// <param name="json">A JSON string.</param>
		/// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
		public static object Deserialize(string json)
		{
			// save the string for debug information
			if (string.IsNullOrEmpty(json))
			{
				return null;
			}

			return Parser.Parse(json);
		}

		sealed class Parser : IDisposable
		{
			const string WORD_BREAK = "{}[],:\"";
			private char[] buffer = new char[256];
			private int bufferIndex;
			private int bufferCount;

			public static bool IsWordBreak(char c)
			{
				return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
			}

			enum TOKEN
			{
				NONE,
				CURLY_OPEN,
				CURLY_CLOSE,
				SQUARED_OPEN,
				SQUARED_CLOSE,
				COLON,
				COMMA,
				STRING,
				NUMBER,
				TRUE,
				FALSE,
				NULL
			};

			StringReader json;

			Parser(string jsonString)
			{
				json = new StringReader(jsonString);
			}

			public static object Parse(string jsonString)
			{
				using (var instance = new Parser(jsonString))
				{
					return instance.ParseValue();
				}
			}

			public void Dispose()
			{
				json.Dispose();
				json = null;
			}

			Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> table = new Dictionary<string, object>();

				// ditch opening brace
				Skip();

				// {
				while (true)
				{
					switch (NextToken)
					{
						case TOKEN.NONE:
							return null;
						case TOKEN.COMMA:
							continue;
						case TOKEN.CURLY_CLOSE:
							return table;
						default:
							// name
							string name = ParseString();
							if (name == null)
							{
								return null;
							}

							// :
							if (NextToken != TOKEN.COLON)
							{
								return null;
							}
							// ditch the colon
							Skip();

							// value
							table[name] = ParseValue();
							break;
					}
				}
			}

			private void Skip()
			{
				if (bufferIndex >= bufferCount)
				{
					ReadBuffer();
				}

				bufferIndex++;
			}

			List<object> ParseArray()
			{
				List<object> array = new List<object>();

				// ditch opening bracket
				Skip();

				// [
				var parsing = true;
				while (parsing)
				{
					TOKEN nextToken = NextToken;

					switch (nextToken)
					{
						case TOKEN.NONE:
							return null;
						case TOKEN.COMMA:
							continue;
						case TOKEN.SQUARED_CLOSE:
							parsing = false;
							break;
						case TOKEN.COLON:
							parsing = false; //invalid array: prevent infinite loop
							break;
						default:
							object value = ParseByToken(nextToken);

							array.Add(value);

							break;
					}
				}

				return array;
			}

			object ParseValue()
			{
				TOKEN nextToken = NextToken;
				return ParseByToken(nextToken);
			}

			object ParseByToken(TOKEN token)
			{
				switch (token)
				{
					case TOKEN.STRING:
						return ParseString();
					case TOKEN.NUMBER:
						return ParseNumber();
					case TOKEN.CURLY_OPEN:
						return ParseObject();
					case TOKEN.SQUARED_OPEN:
						return ParseArray();
					case TOKEN.TRUE:
						return true;
					case TOKEN.FALSE:
						return false;
					case TOKEN.NULL:
						return null;
					default:
						return null;
				}
			}

			string ParseString()
			{
				StringBuilder s = new StringBuilder();
				char c;

				// ditch opening quote
				Skip();

				bool parsing = true;
				while (parsing && ReadBuffer())
				{
					if (bufferCount == 0)
					{
						break;
					}

					// Copy up to special case
					int copyIndex;
					for (copyIndex = bufferIndex; copyIndex < bufferCount; copyIndex++)
					{
						if (buffer[copyIndex] == '"' || buffer[copyIndex] == '\\')
						{
							break;
						}
					}

					if (copyIndex > bufferIndex)
					{
						s.Append(buffer, bufferIndex, copyIndex - bufferIndex);
						bufferIndex = copyIndex;
					}

					// If buffer was empty start from beginning (refill buffer)
					if (bufferIndex == bufferCount)
					{
						continue;
					}

					// Handle special case
					c = NextChar;
					switch (c)
					{
						case '"':
							parsing = false;
							break;
						case '\\':
							if (bufferCount == 0)
							{
								parsing = false;
								break;
							}

							c = NextChar;
							switch (c)
							{
								case '"':
								case '\\':
								case '/':
									s.Append(c);
									break;
								case 'b':
									s.Append('\b');
									break;
								case 'f':
									s.Append('\f');
									break;
								case 'n':
									s.Append('\n');
									break;
								case 'r':
									s.Append('\r');
									break;
								case 't':
									s.Append('\t');
									break;
								case 'u':
									var hex = new char[4];

									for (int i = 0; i < 4; i++)
									{
										hex[i] = NextChar;
									}

									s.Append((char)Convert.ToInt32(new string(hex), 16));
									break;
							}
							break;
						default:
							s.Append(c);
							break;
					}
				}

				return s.ToString();
			}

			object ParseNumber()
			{
				string number = NextWord;

				if (number.IndexOf('.') == -1)
				{
					long parsedInt;
					Int64.TryParse(number, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out parsedInt);
					return parsedInt;
				}

				double parsedDouble;
				Double.TryParse(number, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedDouble);
				return parsedDouble;
			}

			void EatWhitespace()
			{
				while (Char.IsWhiteSpace(PeekChar))
				{
					Skip();
					if (bufferCount == 0)
					{
						break;
					}
				}
			}

			private char PeekChar
			{
				get
				{
					if (bufferIndex >= bufferCount)
					{
						ReadBuffer();
					}
					return bufferCount == 0 ? '\0' : buffer[bufferIndex];
				}
			}

			char NextChar
			{
				get
				{
					if (bufferIndex >= bufferCount)
					{
						ReadBuffer();
					}
					return bufferCount == 0 ? '\0' : buffer[bufferIndex++];
				}
			}

			private string NextWord
			{
				get
				{
					StringBuilder word = new StringBuilder();

					while (bufferCount > 0)
					{
						int breakIndex;
						bool foundBreak = false;
						for (breakIndex = bufferIndex; breakIndex < bufferCount; breakIndex++)
						{
							if (IsWordBreak(buffer[breakIndex]))
							{
								foundBreak = true;
								break;
							}
						}

						if (breakIndex > bufferIndex)
						{
							word.Append(buffer, bufferIndex, breakIndex - bufferIndex);
							bufferIndex = breakIndex;
						}

						if (foundBreak || !ReadBuffer())
						{
							break;
						}
					}

					return word.ToString();
				}
			}

			private bool ReadBuffer()
			{
				if (bufferIndex < bufferCount)
				{
					return true;
				}

				bufferCount = json.Read(buffer, 0, buffer.Length);
				bufferIndex = 0;
				return bufferCount > 0;
			}

			TOKEN NextToken
			{
				get
				{
					EatWhitespace();

					if (bufferCount == 0)
					{
						return TOKEN.NONE;
					}

					switch (PeekChar)
					{
						case '{':
							return TOKEN.CURLY_OPEN;
						case '}':
							Skip();
							return TOKEN.CURLY_CLOSE;
						case '[':
							return TOKEN.SQUARED_OPEN;
						case ']':
							Skip();
							return TOKEN.SQUARED_CLOSE;
						case ',':
							Skip();
							return TOKEN.COMMA;
						case '"':
							return TOKEN.STRING;
						case ':':
							return TOKEN.COLON;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case '-':
							return TOKEN.NUMBER;
					}

					switch (NextWord)
					{
						case "false":
							return TOKEN.FALSE;
						case "true":
							return TOKEN.TRUE;
						case "null":
							return TOKEN.NULL;
					}

					return TOKEN.NONE;
				}
			}
		}

		/// <summary>
		/// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
		/// </summary>
		/// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
		/// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
		public static string Serialize(object obj)
		{
			return Serializer.Serialize(obj);
		}

		sealed class Serializer
		{
			StringBuilder builder;

			Serializer()
			{
				builder = new StringBuilder();
			}

			public static string Serialize(object obj)
			{
				var instance = new Serializer();

				instance.SerializeValue(obj);

				return instance.builder.ToString();
			}

			void SerializeValue(object value)
			{
				IList asList;
				IDictionary asDict;
				string asStr;

				if (value == null)
				{
					builder.Append("null");
				}
				else if ((asStr = value as string) != null)
				{
					SerializeString(asStr);
				}
				else if (value is bool)
				{
					builder.Append((bool)value ? "true" : "false");
				}
				else if ((asList = value as IList) != null)
				{
					SerializeArray(asList);
				}
				else if ((asDict = value as IDictionary) != null)
				{
					SerializeObject(asDict);
				}
				else if (value is char)
				{
					SerializeString(new string((char)value, 1));
				}
				else
				{
					SerializeOther(value);
				}
			}

			void SerializeObject(IDictionary obj)
			{
				bool first = true;

				builder.Append('{');

				foreach (object e in obj.Keys)
				{
					if (!first)
					{
						builder.Append(',');
					}

					SerializeString(e.ToString());
					builder.Append(':');

					SerializeValue(obj[e]);

					first = false;
				}

				builder.Append('}');
			}

			void SerializeArray(IList anArray)
			{
				builder.Append('[');

				bool first = true;

				foreach (object obj in anArray)
				{
					if (!first)
					{
						builder.Append(',');
					}

					SerializeValue(obj);

					first = false;
				}

				builder.Append(']');
			}

			void SerializeString(string str)
			{
				builder.Append('\"');

				foreach (var c in str)
				{
					switch (c)
					{
						case '"':
							builder.Append("\\\"");
							break;
						case '\\':
							builder.Append("\\\\");
							break;
						case '\b':
							builder.Append("\\b");
							break;
						case '\f':
							builder.Append("\\f");
							break;
						case '\n':
							builder.Append("\\n");
							break;
						case '\r':
							builder.Append("\\r");
							break;
						case '\t':
							builder.Append("\\t");
							break;
						default:
							int codepoint = Convert.ToInt32(c);
							if ((codepoint >= 32) && (codepoint <= 126))
							{
								builder.Append(c);
							}
							else
							{
								builder.Append("\\u");
								builder.Append(codepoint.ToString("x4", System.Globalization.CultureInfo.InvariantCulture));
							}
							break;
					}
				}

				builder.Append('\"');
			}

			void SerializeOther(object value)
			{
				if (value is int
					|| value is uint
					|| value is long
					|| value is sbyte
					|| value is byte
					|| value is short
					|| value is ushort
					|| value is ulong)
				{
					builder.Append(value.ToString());
				}
				else if (value is float
						 || value is double
						 || value is decimal)
				{
					string numberString;

					// use recommended fastest way to serialize numbers without loss of precision according to .Net documentation
					if (value is float)
					{
						numberString = ((float)value).ToString("G9", System.Globalization.CultureInfo.InvariantCulture);
					}
					else if (value is double)
					{
						numberString = ((double)value).ToString("G17", System.Globalization.CultureInfo.InvariantCulture);
					}
					else
					{
						numberString = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:R}", value);
					}

					builder.Append(numberString);
					if (numberString.IndexOf('.') == -1)
					{
						builder.Append('.');
					}
				}
				else
				{
					SerializeString(value.ToString());
				}
			}
		}
	}
}

