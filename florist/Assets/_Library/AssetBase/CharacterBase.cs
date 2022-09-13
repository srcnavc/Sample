using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterBase : MonoBehaviour
{
    public CharacterData data;
    public IScreenInput ScreenInput;
    public SkinnedMeshRenderer modelMesh;
    public Animator animator;
    public GameObject modelBase,ragdollModel;
    public Ragdoll ragdoll;
    public bool loadMaterialsOnInit;

    public virtual void Initialize(CharacterData data)
    {
        this.data = data;
        ScreenInput = GetComponent<IScreenInput>();
        modelBase = Instantiate(data.model, transform);

        if (data.RagdollModel != null)
        {
            ragdollModel = Instantiate(data.RagdollModel, transform);
            ragdoll = ragdollModel.GetComponent<Ragdoll>();
            ragdollModel.SetActive(false);
        }


        modelBase.layer = gameObject.layer;
        modelMesh = GetComponentInChildren<SkinnedMeshRenderer>();
            if (loadMaterialsOnInit) 
        modelMesh.materials = data.modelMaterials.materials.ToArray();
        animator = modelBase.GetComponent<Animator>();

    }



}
