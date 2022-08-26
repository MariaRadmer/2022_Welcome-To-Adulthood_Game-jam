using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    List<Tilemap> tilemaps;

    [SerializeField]
    PlayerController playerController;



    //--------------------------------------------- Audio
    [SerializeField]
    List<String> autotileName;
    [SerializeField]
    List<AudioClip> autotileAudioclip;
    [SerializeField]
    List<String> pelletName;
    [SerializeField]
    List<AudioClip> audioClip;
    public Dictionary<String, AudioClip> palleteToAudio = new Dictionary<string, AudioClip>();


    [SerializeField]
    AudioSource audio;

    [SerializeField]
    private AudioClip currentFootStep;

    [SerializeField] float maxVolume;
    [SerializeField] float minVolume;

    [SerializeField] float maxPitch;
    [SerializeField] float minPitch;
    //--------------------------------------------- 

    Tilemap currenttilemap;



    private void Awake()
    {

        Vector3Int pos = Vector3Int.RoundToInt(playerController.gameObject.transform.position);
        getCorrectTileMap(pos);

        for (int i = 0; i < pelletName.Count; i++)
        {
            palleteToAudio.Add(pelletName[i] + "_" + currenttilemap.name, audioClip[i]);

        }
        for (int i = 0; i < autotileName.Count; i++)
        {
            palleteToAudio.Add(autotileName[i], autotileAudioclip[i]);

        }
    }


    // Update is called once per frame
    void runAudio()
    {
        updateCurrentFootStep();

        if (playerController.body.velocity.magnitude > 0.05f)
        {
            audio.volume = UnityEngine.Random.Range(minVolume, maxVolume);
            audio.pitch = UnityEngine.Random.Range(maxPitch, minPitch);

            audio.PlayOneShot(currentFootStep, 1.0f);
        }
    }


    void getCorrectTileMap(Vector3Int pos)
    {
        

        foreach (var tilemap in tilemaps)
        {
            
            if (tilemap.GetTile(pos) != null)
            {
                currenttilemap = tilemap;
                
                return;
            }

        }

    }


    void updateCurrentFootStep()
    {
        Vector3Int pos = Vector3Int.RoundToInt(playerController.gameObject.transform.position);

        try
        {
            getCorrectTileMap(pos);

            TileBase tile = currenttilemap.GetTile(pos);
            String currentKey = tile.name;
            //Debug.Log(currentKey);


            if (palleteToAudio.ContainsKey(currentKey))
            {
                currentFootStep = palleteToAudio[currentKey];
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError(ex);
        }
    }
}