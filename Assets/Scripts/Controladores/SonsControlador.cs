using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SonsControlador : MonoBehaviour
{
    private List<AudioSource> sourcesAtuaisAmbiente = new List<AudioSource>();
    private List<AudioSource> sourcesAtuais = new List<AudioSource>();

    private List<FadesAtuais> fadesAtuais = new List<FadesAtuais>();

    public void TocarSom(AudioClip som, float volume)
    {
        AudioSource source = sourcesAtuais.Find(a => a.clip == som || a.isPlaying == false);

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.volume = volume;
            source.loop = false;
            source.playOnAwake = false;
            source.clip = som;
            sourcesAtuais.Add(source);
        }

        source.Play();
    }

    public void FadeSom(AudioClip som, float duracao, float volume)
    {
        AudioSource source = sourcesAtuaisAmbiente.Find(a=>a.clip == som);

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.volume = 0f;
            source.loop = true;
            source.clip = som;
            sourcesAtuaisAmbiente.Add(source);
        }

        if (!source.isPlaying)
        {
            source.Play();
        }

        FadesAtuais fade = fadesAtuais.Find((x)=>x.source == source);

        if (fade != null)
        {
            StopCoroutine(fade.operacao);
            fadesAtuais.Remove(fade);
        }

        IEnumerator fadeOperacao = FazerFade(source, duracao, volume);

        FadesAtuais novoFade = new FadesAtuais(source, fadeOperacao);

        fadesAtuais.Add(novoFade);

        StartCoroutine(fadeOperacao);
    }

    public void DesligarTudo()
    {
        foreach(AudioSource s in sourcesAtuaisAmbiente)
        {
            FadeSom(s.clip, 2f, 0f);
        }
    }

    private IEnumerator FazerFade(AudioSource source, float duracao,float volume)
    {
        float t = 0f;
        float originalVolume = source.volume;

        while (t < 1f)
        {
            t += Time.fixedDeltaTime / duracao;

            source.volume = Mathf.Lerp(originalVolume, volume, t);

            yield return new WaitForEndOfFrame();
        }

        source.volume = volume;
        if (source.volume <= 0.01f)
        {
            source.Stop();
        }

        fadesAtuais.Remove(fadesAtuais.Find((x) => x.source == source));
    }
}

class FadesAtuais
{
    public AudioSource source;
    public IEnumerator operacao;

    public FadesAtuais(AudioSource source, IEnumerator operacao)
    {
        this.source = source;
        this.operacao = operacao;
    }
}
