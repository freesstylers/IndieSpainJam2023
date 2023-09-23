using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMOD.Studio;
namespace FMODUnity
{
    public class AudioScript : MonoBehaviour
    {
        #region Variables 

        public EventInstance MusicaDia;
        public EventInstance MusicaNoche;

        public EventReference MusicaDia_Ref;
        public EventReference MusicaNoche_Ref;

        public EventReference Escribir;
        public EventReference AbrirPuerta;
        public EventReference CerrarPuerta;
        public EventReference Caminar;


        public HORARIO horarioActual = HORARIO.DIA;

        private float fmodParam = 0;

        public enum Escenario
        {

            PLAZA, //La plaza
            BAR, //La taberna
            BOSQUE, //El bosque
            TIENDA, //La tienda
            TENSION, //USar esta cuando haya un momento tenso, en plan asesinato o el final del juego. ¡¡¡¡¡SOLO PONER POR LA NOCHE!!!!!!
            IGLESIA, //Iglesia
            CASABUELA //Casa de la puta vieja

        }

        public enum HORARIO
        {

            DIA,
            NOCHE

        }

        private void Start()
        {
            MusicaDia = RuntimeManager.CreateInstance(MusicaDia_Ref);
            MusicaNoche = RuntimeManager.CreateInstance(MusicaNoche_Ref);
        }
        #endregion

        #region Public Methods
        //Llamar a esto cada vez que se transicione de noche a dia o de dia a noche
        public void PlayMusic(HORARIO hora)
        {
            FMODUnity.RuntimeManager.PauseAllEvents(true);
            if (horarioActual == HORARIO.DIA)
            {
                Crossfade(2, MusicaNoche, MusicaDia);
            }
                
            else
            {
                Crossfade(2, MusicaDia, MusicaNoche);
            }

            horarioActual = hora;
        }

        //Llamar a esto cuando se quiera hacer play de un unico sonido
        public void PlaySound(EventReference eventSound)
        {
            FMODUnity.RuntimeManager.PlayOneShot(eventSound);

        }
        //Llamar cada vez que se cambie de escenario. Previous escenario es del que vienes, next escenario al que vas
        public void CambiarEscenario(Escenario previousEscenario, Escenario nextEscenario)
        {
            float initNew = 0; float destNew = 0; float initOld = 0; float destOld = 0;

            switch (previousEscenario)
            {
                case Escenario.BAR:
                    initOld = 1;
                    destOld = 0;
                    break;
                case Escenario.BOSQUE:
                    initOld = 5;
                    destOld = 4;
                    break;
                case Escenario.IGLESIA:
                    initOld = 3;
                    destOld = 2;
                    break;
                case Escenario.PLAZA:
                    initOld = 0;
                    destOld = 0;
                    break;
                case Escenario.TIENDA:
                    initOld = 2;
                    destOld = 1;
                    break;
                case Escenario.TENSION:
                    initOld = 2;
                    destOld = 1;
                    break;
                case Escenario.CASABUELA:
                    initOld = 4;
                    destOld = 3;
                    break;

                default:

                    break;
            }

            switch (nextEscenario)
            {

                case Escenario.BAR:
                    initNew = 0;
                    destNew = 1;
                    break;
                case Escenario.BOSQUE:
                    initNew = 4;
                    destNew = 5;
                    break;
                case Escenario.IGLESIA:
                    initNew = 2;
                    destNew = 3;
                    break;
                case Escenario.PLAZA:
                    initNew = 0;
                    destNew = 0;
                    break;
                case Escenario.TIENDA:
                    initNew = 1;
                    destNew = 2;
                    break;
                case Escenario.TENSION:
                    initOld = 1;
                    destOld = 2;
                    break;
                case Escenario.CASABUELA:
                    initNew = 3;
                    destNew = 4;
                    break;

                default:
             
                    break;
            }

            CambiarVariableLocalización(initNew, destNew, initOld, destOld);
        }

        #endregion
        #region Utilidades


        private IEnumerator CambiarVariableLocalización(float initNew, float destNew, float initOld, float destOld)
        {
            fmodParam = initOld;
            while (fmodParam > destOld) {
                fmodParam -= 0.1f;

                if(horarioActual == HORARIO.DIA) MusicaDia.setParameterByName("Localizacion", fmodParam);

                else MusicaNoche.setParameterByName("Localizacion", fmodParam);

                yield return null;
            }

            fmodParam = initNew + 0.05f;

            while (fmodParam <= initNew)
            {
                fmodParam += 0.1f;

                if (horarioActual == HORARIO.DIA) MusicaDia.setParameterByName("Localizacion", fmodParam);

                else MusicaNoche.setParameterByName("Localizacion", fmodParam);

                yield return null;
            }
        }


        protected void Crossfade(float fadeDuration, EventInstance _in, EventInstance _out)
        {
            StartCoroutine(CrossfadeCoroutine(fadeDuration, _in, _out));
        }

        private IEnumerator CrossfadeCoroutine(float fadeDuration, EventInstance _in, EventInstance _out)
        {
            float startVolumeFadeOut;
            float startVolumeFadeIn;

            _out.getVolume(out startVolumeFadeOut);
            _in.getVolume(out startVolumeFadeIn);

            float startTime = Time.time;
            float endTime = startTime + fadeDuration;

            while (Time.time < endTime)
            {
                float elapsed = Time.time - startTime;
                float fraction = Mathf.Clamp01(elapsed / fadeDuration);

                // Update volumes for both instances.
                _out.setVolume(Mathf.Lerp(startVolumeFadeOut, 0f, fraction));
                _in.setVolume(Mathf.Lerp(startVolumeFadeIn, 1f, fraction));

                yield return null;
            }

            // Ensure the volumes are at the final state.
            _out.setVolume(0f);
            _in.setVolume(1f);

            // Stop the fading out instance (optional).
            _out.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _in.start();
        }
        #endregion
        #region Test
        bool dia = true;
        bool bar = false;
        public void testPlaySound()
        {
            PlaySound(Escribir);

        }
        public void testPlayMusic()
        {
            if (dia)
                PlayMusic(HORARIO.DIA);
            else
                PlayMusic(HORARIO.NOCHE);

            dia = !dia;
        }
        public void testCambiarEscenario()
        {
            if (bar)
                CambiarEscenario(Escenario.PLAZA, Escenario.BAR);
            else
                CambiarEscenario(Escenario.BAR, Escenario.PLAZA);

            bar = !bar;

        }

        #endregion

    }
}
