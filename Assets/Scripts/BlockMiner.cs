using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMiner : MonoBehaviour
{
    private int life = 3;
    [SerializeField]
    private int minePhase = 0;
    private float repairtime=0.7f;
    private float repairTimeFull=0.7f;
    [SerializeField]
    private GameObject droppedItem;
    [SerializeField]
    private int droppedItemNumber=0;
    private Animator[] breakAnim;
    // Start is called before the first frame update
    void Start()
    {
        breakAnim= gameObject.GetComponentsInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(breakAnim[0].enabled && minePhase<=0)
        {
            foreach (var anim in breakAnim)
            {
                anim.enabled = false;
                anim.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
        }
        if (breakAnim[0].enabled)
        {
            foreach (var anim in breakAnim)
            {
                anim.SetInteger("Phase", minePhase);
            }
        }
        if (minePhase >= life)
        {
            gameObject.SetActive(false);
            for(var i = 0; i < droppedItemNumber; i++)
            {
                Instantiate(droppedItem, transform.position, transform.rotation);
            }
            minePhase = 0;
        }
        else if(minePhase>0 &&repairtime<=0)
        {
            minePhase--;
            repairtime = repairTimeFull;
        }
        else if(minePhase > 0 && repairtime >0)
        {
            repairtime -= Time.deltaTime;
        }
    }
    public void IncreaseMinePhase()
    {
        if (!breakAnim[0].enabled)
        {
            foreach (var anim in breakAnim)
            {
              
                anim.enabled = true;
            }
        }
        minePhase++;
        repairtime = repairTimeFull;
    }
}
