/*
* Filename: PowerUp.cs
* Developer: James Lasso
* Purpose: Superclass for powerup/pickup system
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* PowerUp Class
* Default values and characteristics of all powerups and pickups
*
* Member variables: none
* 
*/
public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public bool expiresImmediately;
    public GameObject m_specialEffect;
    [SerializeField]
    public AudioClip m_soundEffect;
    protected AudioSource m_soundEffectSource;
    protected PowerUpState powerUpState;

    // keep reference of player here
    // protected playerStuff playerstuff;

    /* Function to keep track of what state the powerup is in.
    *
    * Parameters: none
    *
    * Returns:
    * enum -- current state
    */
    protected enum PowerUpState
    {
        InAttractMode, IsCollected, IsExpiring
    }

    /* Function to set the state the powerup is in when it is intially spawned.
    *  Powerups start in 'attract mode' by default
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void Start()
    {
        powerUpState = PowerUpState.InAttractMode;

        m_soundEffectSource = gameObject.AddComponent<AudioSource>();
        m_soundEffectSource.clip = m_soundEffect;
        m_soundEffectSource.volume = 0.5f; 
        m_soundEffectSource.spatialBlend = 0;
        m_soundEffectSource.maxDistance = 25.0f;
        m_soundEffectSource.rolloffMode = AudioRolloffMode.Linear;
    }

    /* Function for 3D object interaction.
    *
    * Parameters:
    * Collider other -- any other collider than itself
    *
    * Returns: none
    */
    protected virtual void OnTriggerEnter (Collider collision)
    {
            PowerUpCollected(collision.gameObject);
    }

    /* Function handles when powerup is collected by the player.
    *  Special effects and payload are then called.
    *
    * Parameters:
    * gameObjectCollectingPowerUp -- what is collecting powerup
    *
    * Returns: none
    */
    protected virtual void PowerUpCollected(GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }

        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;

        PowerUpEffects(); // Collect special effects

        PowerUpPayload(); // Call paylod
    }

    /* Function to instantiate visual effects and sound
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpEffects()
    {
        if (m_specialEffect != null)
        {
            Instantiate (m_specialEffect, transform.position, transform.rotation, transform);
        }

        if (m_soundEffect != null)
        {
            m_soundEffectSource.Play();
        }
    }

    /* Function to apply the powerups payload to the player
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpPayload()
    {
        Debug.Log("Power Up collected, applying payload for: " + gameObject.name);

        // if instant despawn immmediately
        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
    }

    /* Function to send a message that the powerup has expired
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpHasExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;

        // send message powerup has expired
        Debug.Log("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay();
    }

    /* Function to destroy the powerup.
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void DestroySelfAfterDelay()
    {
        //Destroy(transform.parent.gameObject);
        Debug.Log("PowerUp#DestroySelfAfterDelay: I have been destroyed");
        Destroy(gameObject, 0.5f);
    }
}
/*
   `    `    `    `  ` `  `` ```` `````.`.`............-.------------;-;,,,,,:,::::::::_
`    `    `    `    `   ``    ``.`  ``.` ```  ...-.-..-.-.-.--------,-,-,,,,:,:::,:::::_
  `    `    `    ``  ` `  ` `.:;.`      ` `-_-` `.`` ``.-----------;-;,,,,,:,:,:::::_:__
    `    `    `    ``  ``.-..`.-.`-``` ``..`` .!, ```   ``..----,-,-,,,,:,:,:,:::::::__;
 `    `    `    ` ` ``.`   `  `.``    `.``..`.````` ``` ````.--;-;-,,,,:,:::,::::::_____
   `    `    `  ` ` ``````.`..`` `      `     `` ..`  `` ``` .,-,,,,,,:,:,:::::::____;_;
`    `    `   ``   ..```     `` ..``    ```.    ` `    `  `. `-,,,,,,:,:,:::::::________
  `    `   ` `  ` .` ` ``` ``  ` `.  `     `  `` `.` `` `` `. .,,,:,:::,::::::_____;__;_
    `    ``   `` .-         `--   `   `.```..`  ````     `` ```.,:,:,:::::::_________;_;
 `    ` `  ` `` .``  `  ``  `  ..`       ``````       ``` ` ` ``:,:,::::::::___;_;_;;_;_
   `  ` `  `   `` `.   --.       .  ````   `    `  ```  `    `  ,:,::::::__________;_;;;
`   ``   ```````         .    `   . `````    `    ```  `    `   -::::::::__;_;_;__;_;_;_
   `  ` ` ` ``   `         `   `` ``..-`      ``   . `   ``  ` `.::::::________;_;_;_;;;
`   ``  `  `````   ```.-__,-  ` `-,~=!=~:.---`   ``` ```   `    .::::____;_;;_;_;_;_;_;;
 ` `  `` `` `. ``  `!3K%$B4vY;-`-!aKdHG4afYJL=!_`       `.` `  `.::__________;_;_;;;;;;;
 ` ` ` ` ```````` .L8@@&@@@WETvJFKWWWW$#dKEn3aFYL, ` `     `.`` .____;_;_;;_;_;_;_;;;;;;
`     ```` ``` ` .l@@@@&&&@@@BN#8WW@@WMkGGKGySSFs~.`` ``    ` `..______;_;_;_;;;;;;;;;;;
  ` ``` ``````` `7W@@@@&@@&&@@W@@@WWWW$MHwE4AAy5Cr,.       ``` `-__;_;;_;_;_;_;_;;;;;;;~
 ``` ``````.`.```n@@@@@WWW@@WW8%%#NNdNMMHkGEwE3n5C~-` ``   `   `.:___;_;_;;;;;;;;;;;;~~~
` ```` ```.`.`.`.M@@@@@8B8W@%NM$MdGAAEyfnSynnnnf5FJ:`    `---`  `_;_;_;_;_;_;;;;;;;~~~~~
``` `````.`.`....8@@@WW@WW8HKkwwAy5VVTCCTVF5F5FFCCr,``  ``..``3W3;_;_;;;;;;;;;;;;;~~~~~~
 `````.`.`.....-`k&@@WB8%%#Ga4EEa3fFTYYJsTVFFFFFFVL>;.``     Vl-nF__;_;_;_;;;;;;~~~~~!~~
`` ``.`.`.......`s&@@W8%MmK4ySnfSa5TJLlJYsVVTVFFFFV>;_``. `;J~YT_k_;;;;;;;;;;;;~~~~~~~~!
````.`.`.......--_&@WWW@B#NdkAySaynTJlJTFFCCsFFVVFF5Cl!-` .v;rvlsKl;;_;;;;;;;~~~~~~~!~~~
`.`.`...........-;@@@@@@WB#kkanyEGGfFsVnnn5Fnnay4A4AkmGL--`~>l,_vd=;;;;;;;;;~~~~~!~~~~!~
``.`.......-.-..-.@@@W@@88#GNHEEmdNkFY5AyaAaannVsnGAAkEF=.:Sf.;;YM!;;;;;;;~~~~~~~~~!~~!~
`.`..........-.--:%W8%@888d4dk3wdNkwrVaEST!;.-.--,!7LSyma:v#lS!3GF;;;;;;~~~~~~~~!~~~!!~~
.........-.--.-.!!77~!7_lddw$VYYkfYYFFl_.--`       `..a%374%T!7E3~;;;;;~~~~~~!~~~!~!~~!~
`..........-.--..`-.` `;~;.`.._7Fl---_,          `` `,H$$kK#TL>$A;~;~~~~~~!~~~~!!~~~!!~~
.......-..-.-.--._:~- ``  ` `-!CVJ!r-`            ``.SKmmNNMCsSfn;~;~~~~~~~~!~!~~!~!~~!~
.......-.-.------.`;-. `       :~>_;  `         ```!4wkw4dM$YvTwJ;~~~~~~~!~~~~!~~!~!~~!~
...-..-.-.-.-.----::.,-```    ;y#v>_!`          `!A4AAA4wmM8T73L~~~~~~!~~~~!~~~!!~~~!!~~
.....-.-----------_dHr  ` ```!r@%fv=!;-`  ` `-!T333ySy44wmMWy7n@;-~~~~~~!!~~!~!~~!~!~~!~
.--.-.-.-.---------VBKkafJYJTANW#nYlvll7rJY7=vJsC5f333AwkmM8yFfV. ;~~!~~~~!!~~~!!~~~!!~~
.-.-.----------;-;-!$H4wKN#md#m%#EJvlLLJTFFVJlLJJYsC5fy4Gm#Mssv.``,~~~~!~!~~!~!~~!~!~~!~
-.-.-.------,-,-,,,-EBdk4GkGAEkW@yYJ7=vLJsFFCsYJLlJFnn3EGd#3vv-````;!~~!~!~~!~!~~!~!~~!~
.------------;-;,,,,:WBNGkw4S5k@W3CTv5!_!vYsTYJYJYT5fn3AGGw=!- `` `-~!!~~~!!~~~!!~~~!!~~
-.-.--------,-,,,,:,,7@WdmKEsVW@dFV5a4T;!;!lTsYYYT3F5n3yaJ;;-`````  -~~!~!~~!~!~~!~!~~!~
---------;-;-,,,,:,::,n@8#kar34Wf~;=sFf~Jl;!vYYTf3VC53nTJ;`.` `` ````-~~~~!!~~~!!~~~!!~~
--------,-,,,,,,:,::::_kW8NaAW@%m;:,-,_!YVV~~lYT5fCCFFYJ!``   ````````,!~!~~!~!~~!~!~~!~
-------;-;,,,:,:,:,::::~MWdw%W@W#A!~_;>TJLJVJ~7JT5FVYJv:` ````` `` ` ``:!!~~!~!~~!~!~~!~
----,-,-,,,,:,:::,::::_:>#dG%wdwkA:;~>rr~~__!>~rYTTCYv_` `` `   ````````-~!!~~~!!~~~!!~~
-;-;-;,,,,,:,:,::::::___:l$dnYaY!~lYs;;;;_..`,>vlvJL=-       ``` ````.`.`.~~~~!~~!~!~~!~
--,-,,,,,,:,:,:::::::_____CBaYCl:``    `.:;>l;_!=r7~. ```` ``    `.```.`.` ,~~~!!~~~!!~~
-;-,,,,:,:,:,::::_:______;;w8@8KEf7!7!!J!;!>>;,;!~~. ```  `  `````````` ````.~!~~!~!~~!~
,,,,,,:,:::,::::::___;_;___!HW%GY=>~___,:;;_:,:;~,`` `  ` ````` ````````` `` -~~~!~!~~!~
,,,,,:,:,:::::_:________::-..k8aT!-.`.-:__;_:;;!, `   ``  `  ````..``` `   `  -!!~~~!!~~
,,:,:,:,:::::::____;_;_::_,-.`BWNGfFTJ=~>>!~~!!:    `   `  ``` `..```         ``:~~!~~!~
,:,:,:,::::::_________~!!;,.` .8BGFsVYJLlL>!>;.  `    ` ```````. ``            ` -~~!!~~
:,:,:::::::____;_;;_;>>~;_-``  .CSv=~==!!;:;.`` `  `  `` ````````      `    ` `  `.;~~!~
,:::::::::_______;_~>;;_-.` `   ~F~;;;_,--_=,`.``  ````````...``    `  `  `` ``  `` -~!~
:::::::::__;_;_;;;~~~_,.,,-` ``  TmN3J~;;~v!-_-  ```......-.         `````  `````.````:~
::::::_________;~~_,->vv~--``  ``:MkHT=~~7vvY,`.` `..---..``       ` ``............---`;
:::::____;_;;;;_~:-~vr~_;,`  ```-`wMw4Y=~!r3-`.`  .---,..`     `  ``...-.-.-..-.-.------
:_:________;_;_;,!rr~-:!~.` ` `-,.G@BET=~=w;..` `..---..`    ``.`````........-----------
:____;_;_;__;_;,~v!,,:;!` ` `_---;@WGr>rlkv.-- `..---..    .--.-.`.....--.-.-.-.-.------
_______;_;_;_;!>>!-,;!~``   ,=--,;8WA~7vAG-,-``-------``.---...................---------
_;_;____;_;_~~~!;-:~!~.`` ` >=--:,d%a~_Ym-,-.`.-,--,- `.---.-.--.`.......`.`...--.-.----
_______;_;;;!!~;-:~~!. ` ` ,ll_--:_fF;_J,,:-`.-;--,- -_,-,--`..__------...`.`...`..-----
_;__;_;_;-:~!~;::~~!-  ``  !7lY;---.:~=,,_,.`.,----`:_,......~;---,-.```.--....`...-..--
_;_;_;_;;!r!~~_~r>~_` `   ,=rvvs!-::,~:-:_- .,,-.: ,:-...--:~-`.``` `        `....`....-
_;;_;_;;vYY!~_;77!;-`.`  `;~7vv7v!:__,-:;-.-:,--, .-,..-.`.,.`  ``             ` ..``...
_;_;;;_!YY7;!=rv!;-..`   :~rr777>~_:-:,_,--;,--;  --.-`  `   `           `  ` `  ``..`.-
;_;_;_;;=v!;~;>>~....   ._~7vr7vv!:,_:,.--,---:` `-_. ``  ```   `    ``     ``` ``.--...
*/
