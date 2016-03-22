using SDL2;
using System;
using System.Collections.Generic;

namespace src
{
    enum sound_type
    {
        SOUND_MUSIC,
        SOUND_SFX
    }
    class SoundManager
    {
        private static SoundManager pInstance;
        private Dictionary<string, IntPtr> sfxs = new Dictionary<string, IntPtr>();
        private Dictionary<string, IntPtr> musics = new Dictionary<string, IntPtr>();

        public static SoundManager Instance()
        {
            if (pInstance == null)
            {
                pInstance = new SoundManager();
                return pInstance;
            }
            return pInstance;
        }

        public bool load(string fileName, string id, sound_type type)
        {
            //IntPtr pMusic = IntPtr.Zero;
            if(type == sound_type.SOUND_MUSIC)
            {
                IntPtr pMusic = SDL_mixer.Mix_LoadMUS(fileName);
            
                if(pMusic == IntPtr.Zero)
                {
                    Console.WriteLine("Could not load music");
                    return false;
                }
                musics[id] = pMusic;
                return true;
            }
            else if (type == sound_type.SOUND_SFX)
            {
                IntPtr pChunk = SDL_mixer.Mix_LoadWAV(fileName);
                if(pChunk == IntPtr.Zero)
                {
                    Console.WriteLine("Could not load SFX");
                    return false;
                }
                sfxs[id] = pChunk;
                return true;
            }
            return false;
        }

        public void playSound(string id, int loop)
        {
            SDL_mixer.Mix_PlayMusic(musics[id], loop);
        }
        public void playMusic(string id, int loop)
        {
            SDL_mixer.Mix_PlayChannel(-1, sfxs[id], loop);
        }
        
        private SoundManager()
        {
            // AUDIO_S16
            SDL_mixer.Mix_OpenAudio(22050, SDL.AUDIO_S16, 2, 4096);
        }

        ~SoundManager()
        {
            SDL_mixer.Mix_CloseAudio();
        }

       /* private SoundManager(ref SoundManager)
        {

        }

        private SoundManager operator =(SoundManager)
        {

        }*/
    }
}
