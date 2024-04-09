using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    public GameObject tetrahedron; // prefab da camrera
    public GameObject[] vetGameObj = new GameObject[24];
    GameObject pai;
    Vector3 m_Center;
    // Use this for initialization
    int i = -1;

    public GameObject[] vertices = new GameObject[4];
    public GameObject[] centers = new GameObject[4];

    Vector3 Posit(int x, int a)
    {
        return vetGameObj[x].gameObject.transform.GetChild(a - 1).transform.position;
    }

    void NovoTetra(Vector3 posicao)
    {
        i++;
        GameObject _tetra = Instantiate(tetrahedron, posicao, Quaternion.identity);

        Vector3 _p0 = _tetra.GetComponent<createTetra>().p0 + posicao;
        GameObject p0 = new GameObject();
        p0.transform.position = _p0;

        Vector3 _p1 = _tetra.GetComponent<createTetra>().p1 + posicao;
        GameObject p1 = new GameObject();
        p1.transform.position = _p1;

        Vector3 _p2 = _tetra.GetComponent<createTetra>().p2 + posicao;
        GameObject p2 = new GameObject();
        p2.transform.position = _p2;

        Vector3 _p3 = _tetra.GetComponent<createTetra>().p3 + posicao;
        GameObject p3 = new GameObject();
        p3.transform.position = _p3;

        p0.transform.parent = _tetra.transform;
        p1.transform.parent = _tetra.transform;
        p2.transform.parent = _tetra.transform;
        p3.transform.parent = _tetra.transform;

        vetGameObj[i] = _tetra;
    }

    //Face verde
    void NovoTetraInvertidoF0(Vector3 posicao)
    {
        NovoTetra(posicao);
        Vector3 p1 = Posit(i, 1);
        Vector3 p2 = Posit(i, 2);
        Vector3 _p = (p1 + p2) / 2;

        GameObject p = new GameObject();
        p.transform.position = _p;

        vetGameObj[i].transform.parent = p.transform;
        p.transform.rotation = Quaternion.Euler(new Vector3(36.87f, 0f, 180f));

        vetGameObj[i].transform.parent = null;
        Destroy(p);
    }

    //Face vermelha
    void NovoTetraInvertidoF1(Vector3 posicao)
    {
        NovoTetra(posicao);
        Vector3 p1 = Posit(i, 1);
        Vector3 p2 = Posit(i, 3);
        Vector3 _p = (p1 + p2) / 2;

        GameObject p = new GameObject();
        p.transform.position = _p;

        Vector3 d = p2 - p1;
        p.transform.rotation = Quaternion.LookRotation(d);

        vetGameObj[i].transform.parent = p.transform;
        p.transform.rotation = Quaternion.Euler(new Vector3(180f, 30f, 36.87f));

        vetGameObj[i].transform.parent = null;
        Destroy(p);
    }

    //Face Amarela
    void NovoTetraInvertidoF2(Vector3 posicao)
    {
        NovoTetra(posicao);
        Vector3 p1 = Posit(i, 2);
        Vector3 p2 = Posit(i, 3);
        Vector3 _p = (p1 + p2) / 2;

        GameObject p = new GameObject();
        p.transform.position = _p;

        Vector3 d = p2 - p1;
        p.transform.rotation = Quaternion.LookRotation(d);
        GameObject objt = new GameObject();

        vetGameObj[i].transform.parent = p.transform;
        p.transform.rotation = Quaternion.Euler(new Vector3(180f, -30f, -36.87f));

        vetGameObj[i].transform.parent = null;
        Destroy(p);
    }

    //Face azul
    void NovoTetraInvertidoF3(Vector3 posicao)
    {
        NovoTetra(posicao);
        Vector3 _p = Posit(i, 1);

        GameObject p = new GameObject();
        p.transform.position = _p;

        vetGameObj[i].transform.parent = p.transform;
        p.transform.rotation = Quaternion.Euler(0f, 60f, 0f);

        vetGameObj[i].transform.parent = null;
        Destroy(p);
    }

    void TetraMagico()
    {
        NovoTetra(new Vector3(0, 0, 0));

        GameObject v0 = new GameObject();
        v0.transform.position = Posit(0, 1);

        NovoTetra(Posit(0, 2));
        NovoTetra(Posit(0, 3));
        NovoTetra(Posit(1, 2));

        GameObject v1 = new GameObject();
        v1.transform.position = Posit(3, 2);

        NovoTetra(Posit(1, 3));
        NovoTetra(Posit(2, 3));

        GameObject v2 = new GameObject();
        v2.transform.position = Posit(5, 3);

        NovoTetra(Posit(0, 4));
        NovoTetra(Posit(1, 4));
        NovoTetra(Posit(2, 4));
        NovoTetra(Posit(6, 4));

        GameObject v3 = new GameObject();
        v3.transform.position = Posit(9, 4);

        NovoTetraInvertidoF0(Posit(0, 4));
        NovoTetraInvertidoF0(Posit(1, 4));
        NovoTetraInvertidoF0(Posit(6, 4));
        NovoTetraInvertidoF1(Posit(0, 4));
        NovoTetraInvertidoF1(Posit(2, 4));
        NovoTetraInvertidoF1(Posit(6, 4));
        NovoTetraInvertidoF2(Posit(1, 4));
        NovoTetraInvertidoF2(Posit(2, 4));
        NovoTetraInvertidoF2(Posit(6, 4));
        NovoTetraInvertidoF3(Posit(0, 3));
        NovoTetraInvertidoF3(Posit(1, 3));
        NovoTetraInvertidoF3(Posit(2, 3));

        GameObject c0 = new GameObject();
        c0.transform.position = (v1.transform.position + v2.transform.position + v3.transform.position) / 3;  // Posiciona o GameObject
        Vector3 r0 = c0.transform.position - v0.transform.position;
        v0.transform.rotation = Quaternion.LookRotation(r0);

        GameObject c1 = new GameObject();
        c1.transform.position = (v0.transform.position + v2.transform.position + v3.transform.position) / 3;
        Vector3 r1 = c1.transform.position - v1.transform.position;
        v1.transform.rotation = Quaternion.LookRotation(r1);

        GameObject c2 = new GameObject();
        c2.transform.position = (v0.transform.position + v1.transform.position + v3.transform.position) / 3;
        Vector3 r2 = c2.transform.position - v2.transform.position;
        v2.transform.rotation = Quaternion.LookRotation(r1);

        GameObject c3 = new GameObject();
        c3.transform.position = (v1.transform.position + v2.transform.position + v0.transform.position) / 3;
        Vector3 r3 = c3.transform.position - v3.transform.position;
        v3.transform.rotation = Quaternion.LookRotation(r3);

        vertices[0] = v0;
        vertices[1] = v1;
        vertices[2] = v2;
        vertices[3] = v3;
        centers[0] = c0;
        centers[1] = c1;
        centers[2] = c2;
        centers[3] = c3;
    }


    void Start()
    {
        TetraMagico();

    }


    // Update is called once per frame
    int eixo, nivel, direcao;
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            eixo = 0;
        }
        if (Input.GetKeyDown("w"))
        {
            eixo = 1;
        }
        if (Input.GetKeyDown("e"))
        {
            eixo = 2;
        }
        if (Input.GetKeyDown("r"))
        {
            eixo = 3;
        }


        if (Input.GetKeyDown("a"))
        {
            nivel = 0;
        }
        if (Input.GetKeyDown("s"))
        {
            nivel = 1;
        }
        if (Input.GetKeyDown("d"))
        {
            nivel = 2;
        }

        if (Input.GetKeyDown("z"))
        {
            direcao = 1;
            RotateTetraMagico( eixo, nivel, direcao);
        }
        if (Input.GetKeyDown("x"))
        {
            direcao = -1;
            RotateTetraMagico(eixo, nivel, direcao);
        }

    }

    void RotateTetraMagico(int eixo, int nivel, int direcao)
    {
        Vector3 posicao;
        switch (nivel)
        {
            default:
                posicao = vertices[eixo].transform.position;
                break;
            case 0:
                posicao = vertices[eixo].transform.position;
                break;
            case 1:
                posicao = (vertices[eixo].transform.position + centers[eixo].transform.position) / 2;
                break;
            case 2:
                posicao = centers[eixo].transform.position;
                break;


        }
        Collider[]hitColliders = Physics.OverlapBox(posicao, new Vector3(5, 5, 0.1f), vertices[eixo].transform.rotation);
        foreach (Collider collider in hitColliders)
        {
         collider.transform.parent = vertices[eixo].transform;
        }
        vertices[eixo].transform.Rotate(Vector3.forward * 120 * direcao);
        foreach (Collider collider in hitColliders)
        {
         collider.transform.parent = null;
        }
    }
}