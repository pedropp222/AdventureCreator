using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SonsControlador : MonoBehaviour
{
    private List<AudioSource> sourcesAtuais = new List<AudioSource>();

    //bool isFading = false;

    private List<FadesAtuais> fadesAtuais = new List<FadesAtuais>();

    public void FadeSom(AudioClip som, float duracao, float volume)
    {
        AudioSource source = sourcesAtuais.Find(a=>a.clip == som);

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.volume = 0f;
            source.loop = true;
            source.clip = som;
            sourcesAtuais.Add(source);
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
        foreach(AudioSource s in sourcesAtuais)
        {
            FadeSom(s.clip, 2f, 0f);
        }
    }

    private IEnumerator FazerFade(AudioSource source, float duracao,float volume)
    {
        //isFading = true;

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
        //isFading = false;
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