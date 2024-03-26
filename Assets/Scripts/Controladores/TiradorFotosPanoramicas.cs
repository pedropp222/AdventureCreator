using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TiradorFotosPanoramicas : MonoBehaviour
{
    public string localGuardarFotos;
    public Material materialBase;
    public GameObject cuboPrefab;
    public FramesJogoControlador framesJogoObjeto;

    Camera cam;


    public void TirarFotos()
    {
        if (localGuardarFotos.Trim().Length == 0)
        {
            return;
        }

        if (!AssetDatabase.IsValidFolder("Assets/"+localGuardarFotos))
        {
            AssetDatabase.CreateFolder("Assets", localGuardarFotos);
        }
        //DirectoryInfo info = Directory.CreateDirectory("Assets/"+localGuardarFotos);

        cam = Camera.main;

        cam.fieldOfView = 90f;

        StartCoroutine(Processo());

      
    }

    public void CriarCubos()
    {
        int pontosIniciais = transform.childCount;

        for (int i = 0; i < pontosIniciais; i++)
        {
            string cName = transform.GetChild(i).name + "_cube";

            if (!framesJogoObjeto.ExisteCubo(cName))
            {
                GameObject cuboNovo = Instantiate(cuboPrefab, framesJogoObjeto.transform);
                cuboNovo.name = cName;
                cuboNovo.transform.position = transform.GetChild(i).position;

                Material frente = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "frenteMaterial.asset");
                Material tras = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "trasMaterial.asset");
                Material cima = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "cimaMaterial.asset");
                Material baixo = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "baixoMaterial.asset");
                Material direita = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "direitaMaterial.asset");
                Material esquerda = AssetDatabase.LoadAssetAtPath<Material>("Assets/" + localGuardarFotos + "/" + cName + "/" + "esquerdaMaterial.asset");

                cuboNovo.GetComponent<MeshRenderer>().SetSharedMaterials(new List<Material> { cima, frente, direita, baixo, esquerda, tras });

                framesJogoObjeto.AdicionarCubo(cuboNovo.GetComponent<CuboFrame>());
            }
            else
            {
                CuboFrame c = framesJogoObjeto.listaCubos[i];
                if (c.transform.position != transform.GetChild(i).position)
                {
                    c.transform.position = transform.GetChild(i).position;
                }
            }
        }
    }

    private IEnumerator Processo()
    {
        yield return StartCoroutine(CapturarFotos()); 
    }

    private IEnumerator CapturarFotos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            string cName = transform.GetChild(i).name;

            if (!AssetDatabase.IsValidFolder("Assets/" + localGuardarFotos+"/"+cName))
            {
                AssetDatabase.CreateFolder("Assets/" + localGuardarFotos, cName);
            }

            cam.transform.position = transform.GetChild(i).position;

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("FRONT");
            cam.transform.rotation = Quaternion.Euler(Vector3.zero);
            yield return new WaitForEndOfFrame();

            GuardarTexturaMaterial(cName,"frente");

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("LEFT");
            cam.transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
            yield return new WaitForEndOfFrame();

            GuardarTexturaMaterial(cName,"esquerda");

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("RIGHT");
            cam.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            yield return new WaitForEndOfFrame();


            GuardarTexturaMaterial(cName,"direita");

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("TOP");
            cam.transform.rotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
            yield return new WaitForEndOfFrame();


            GuardarTexturaMaterial(cName,"cima");

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("DOWN");
            cam.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            yield return new WaitForEndOfFrame();


            GuardarTexturaMaterial(cName,"baixo");

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("BACK");
            cam.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            yield return new WaitForEndOfFrame();

            GuardarTexturaMaterial(cName,"tras");

            Debug.Log("Criadas fotos para o objeto " + cName);
        }      
    }

    void GuardarTexturaMaterial(string cName, string nome)
    {
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();
        tex.wrapMode = TextureWrapMode.Clamp;

        byte[] b = tex.EncodeToPNG();

        File.WriteAllBytes("Assets/" + localGuardarFotos + "/" + cName + "/" + nome + "Imagem.png", b);

        Material novo = new Material(materialBase);

        AssetDatabase.ImportAsset("Assets/" + localGuardarFotos + "/" + cName + "/" + nome + "Imagem.png", ImportAssetOptions.Default);

        Texture2D ntex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/" + localGuardarFotos + "/" + cName + "/" + nome + "Imagem.png");
        ntex.wrapMode = TextureWrapMode.Clamp;

        novo.mainTexture = ntex;

        AssetDatabase.CreateAsset(novo, "Assets/" + localGuardarFotos + "/" + cName + "/" + nome + "Material.asset");
    }

}
