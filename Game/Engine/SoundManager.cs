/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 22.03.2016
 */
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
        private static SoundManager instance;
        private Dictionary<string, IntPtr> sounds = new Dictionary<string, IntPtr>();
        private Dictionary<string, IntPtr> musics = new Dictionary<string, IntPtr>();

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                    return instance;
                }
                return instance;
            }
        }

        public bool Load(string fileName, string id, sound_type type)
        {
            if (type == sound_type.SOUND_MUSIC)
            {
                IntPtr music = SDL_mixer.Mix_LoadMUS(fileName);

                if (music == IntPtr.Zero)
                {
                    Console.WriteLine("Could not load music");
                    return false;
                }
                musics[id] = music;
                return true;
            }
            else if (type == sound_type.SOUND_SFX)
            {
                IntPtr pChunk = SDL_mixer.Mix_LoadWAV(fileName);
                if (pChunk == IntPtr.Zero)
                {
                    Console.WriteLine("Could not load SFX");
                    return false;
                }
                sounds[id] = pChunk;
                return true;
            }
            return false;
        }

        public void PlaySound(string id, int loop = 0)
        {
            SDL_mixer.Mix_PlayChannel(-1, sounds[id], loop);
        }
        public void PlayMusic(string id, int loop = 0)
        {
            SDL_mixer.Mix_PlayMusic(musics[id], loop);

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
    }
}
