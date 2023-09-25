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
        [FMODUnity.BankRef]
        public List<string> Banks = new List<string>();
        public EventInstance MusicaDia;
        public EventInstance MusicaNoche;

        public EventReference MusicaDia_Ref;
        public EventReference MusicaNoche_Ref;

        public EventReference Escribir;
        public EventReference AbrirPuerta;
        public EventReference CerrarPuerta;
        public EventReference Caminar;

        public EventReference[] NarradorEvents;
        private bool[] NarradorEventsBooleans;


        public HORARIO horarioActual = HORARIO.DIA;

        private float fmodParam = 0;
        private bool fading = false;

        private float volumeMusic = 1f;

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
            StartCoroutine(LoadGameAsync());
        }
        #endregion

        #region Public Methods
        public void setVolumeMusic(float vol)
        {
            volumeMusic = vol;
            if (horarioActual == HORARIO.DIA)
                MusicaDia.setVolume(vol);
            else MusicaNoche.setVolume(vol);
        }
        //Llamar a esto cada vez que se transicione de noche a dia o de dia a noche
        public void PlayMusic(HORARIO hora)
        {
            if (horarioActual == HORARIO.NOCHE)
            {
                Crossfade(3, MusicaDia, MusicaNoche);
            }
            else
            {
                Crossfade(3, MusicaNoche, MusicaDia);
                
            }

            horarioActual = hora;
        }

        //Llamar a esto cuando se quiera hacer play de un unico sonido
        public void PlaySound(EventReference eventSound)
        {
            FMODUnity.RuntimeManager.PlayOneShot(eventSound);

        }

        //Para hacer sonar al narrador
        public void PlayNarradorSound(int indice)
        {
            if (!NarradorEventsBooleans[indice])
            {
                PlaySound(NarradorEvents[indice]);
                NarradorEventsBooleans[indice] = true;
            }
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

            StartCoroutine(CambiarVariableLocalización(initNew, destNew, initOld, destOld));
        }

        //Mismo metodo pero en strings que es mas comodo
        public void CambiarEscenario(string previousEscenario, string nextEscenario)
        {
            float initNew = 0; float destNew = 0; float initOld = 0; float destOld = 0;

            switch (previousEscenario)
            {
                case "taberna":
                    initOld = 1;
                    destOld = 0;
                    break;
                case "bosque":
                    initOld = 5;
                    destOld = 4;
                    break;
                case "iglesia":
                    initOld = 3;
                    destOld = 2;
                    break;
                case "plaza":
                    initOld = 0;
                    destOld = 0;
                    break;
                case "emporio":
                    initOld = 2;
                    destOld = 1;
                    break;
                case "tension":
                    initOld = 2;
                    destOld = 1;
                    break;
                case "casa":
                    initOld = 4;
                    destOld = 3;
                    break;

                default:

                    break;
            }

            switch (nextEscenario)
            {

                case "taberna":
                    initNew = 0;
                    destNew = 1;
                    break;
                case "bosque":
                    initNew = 4;
                    destNew = 5;
                    break;
                case "iglesia":
                    initNew = 2;
                    destNew = 3;
                    break;
                case "plaza":
                    initNew = 0;
                    destNew = 0;
                    break;
                case "emporio":
                    initNew = 1;
                    destNew = 2;
                    break;
                case "tension":
                    initOld = 1;
                    destOld = 2;
                    break;
                case "casa":
                    initNew = 3;
                    destNew = 4;
                    break;

                default:

                    break;
            }

            StartCoroutine(CambiarVariableLocalización(initNew, destNew, initOld, destOld));
        }

        #endregion
        #region Utilidades

        IEnumerator LoadGameAsync()
        {
            foreach (var bank in Banks)
            {
                FMODUnity.RuntimeManager.LoadBank(bank, true);
            }

            while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
            {
                yield return null;
            }

            MusicaDia = RuntimeManager.CreateInstance("event:/Musica/Dia");
            MusicaNoche = RuntimeManager.CreateInstance("event:/Musica/Noche");
            MusicaDia.start();
            MusicaNoche.start();
            if (horarioActual == HORARIO.DIA)
                MusicaNoche.setVolume(0.0f);
            else MusicaDia.setVolume(0.0f);

            NarradorEventsBooleans = new bool[30];
            
            for (int i = 0; i < NarradorEventsBooleans.Length; i++) 
                NarradorEventsBooleans[i] = false;
        }
        private IEnumerator CambiarVariableLocalización(float initNew, float destNew, float initOld, float destOld)
        {
            if (!fading)
            {
                fading = true;
                RESULT res;
                fmodParam = initOld;
                while (fmodParam > (destOld + 0.1))
                {
                    fmodParam -= 0.01f;

                    if (horarioActual == HORARIO.DIA) res = MusicaDia.setParameterByName("Loc", fmodParam);

                    else res = MusicaNoche.setParameterByName("Loc", fmodParam);

                    yield return null;
                }

                fmodParam = initNew + 0.05f;

                while (fmodParam < destNew)
                {
                    fmodParam += 0.01f;

                    if (horarioActual == HORARIO.DIA) res = MusicaDia.setParameterByName("Loc", fmodParam);

                    else res = MusicaNoche.setParameterByName("Loc", fmodParam);

                    yield return null;

                }

                fmodParam = destNew;
                if (horarioActual == HORARIO.DIA) res = MusicaDia.setParameterByName("Loc", fmodParam);

                else res = MusicaNoche.setParameterByName("Loc", fmodParam);
                fading = false;
            }
            else yield return null;
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
            // _in.start();
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
            _in.setVolume(volumeMusic);

            // Stop the fading out instance (optional).
            // _out.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
        #endregion
        #region Test
        bool dia = true;
        bool bar = false;
        private float vol;

        public void testPlaySound()
        {
            PlaySound(Escribir);

        }
        public void testPlayMusic()
        {
            if (!dia)
                PlayMusic(HORARIO.DIA);
            else
                PlayMusic(HORARIO.NOCHE);

            dia = !dia;
        }
        public void testCambiarEscenario()
        {
            if (!bar)
                CambiarEscenario(Escenario.TIENDA, Escenario.BAR);
            else
                CambiarEscenario(Escenario.BAR, Escenario.TIENDA);

            bar = !bar;

        }

        #endregion

    }
}
