using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using TMPro;

public class CooldownManager : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI other;
    public IntVariable selectedElement;


    private float _air_1_cd;
    private float _air_2_cd;
    private float _earth_1_cd;
    private float _earth_2_cd;
    private float _fire_1_cd;
    private float _fire_2_cd;
    private float _water_1_cd;
    private float _water_2_cd;

    //hold index values
    private int _hold = 100;

    void FixedUpdate()
    {  
        //Giant switch statement because I couldn't get a freakin' float[] to work properly...
        switch(selectedElement.Value){
            case 0:
                if(_air_1_cd > 0){
                    attack.SetText(_air_1_cd.ToString());
                }else{
                    attack.SetText("");
                    if (_air_1_cd <= 0){
                        CancelInvoke("AirAttackCD");
                        _air_1_cd = 0;
                    }
                }

                if(_air_2_cd > 0){
                    other.SetText(_air_2_cd.ToString());
                }else{
                    other.SetText("");
                    if (_air_2_cd <= 0){
                        CancelInvoke("AirOtherCD");
                        _air_2_cd = 0;
                    }
                }
            break;
            case 1:
                if(_earth_1_cd > 0){
                    attack.SetText(_earth_1_cd.ToString());
                }else{
                    attack.SetText("");
                    if (_earth_1_cd <= 0){
                        CancelInvoke("EarthAttackCD");
                        _earth_1_cd = 0;
                    }
                }

                if(_earth_2_cd > 0){
                    other.SetText(_earth_2_cd.ToString());
                }else{
                    other.SetText("");
                    if (_earth_2_cd <= 0){
                        CancelInvoke("EarthOtherCD");
                        _earth_2_cd = 0;
                    }
                }
            break;
            case 2:
                if(_fire_1_cd > 0){
                    attack.SetText(_fire_1_cd.ToString());
                }else{
                    attack.SetText("");
                    if (_fire_1_cd <= 0){
                        CancelInvoke("FireAttackCD");
                        _fire_1_cd = 0;
                    }
                }

                if(_fire_2_cd > 0){
                    other.SetText(_fire_2_cd.ToString());
                }else{
                    other.SetText("");
                    if (_fire_2_cd <= 0){
                        CancelInvoke("FireOtherCD");
                        _fire_2_cd = 0;
                    }
                }
            break;
            case 3:
                if(_water_1_cd > 0){
                    attack.SetText(_water_1_cd.ToString());
                }else{
                    attack.SetText("");
                    if (_water_1_cd <= 0){
                        CancelInvoke("WaterAttackCD");
                        _water_1_cd = 0;
                    }
                }

                if(_water_2_cd > 0){
                    other.SetText(_water_2_cd.ToString());
                }else{
                    other.SetText("");
                    if (_water_2_cd <= 0){
                        CancelInvoke("WaterOtherCD");
                        _water_2_cd = 0;
                    }
                }
            break;
        }

/*          This code would've  been preferable but shit happens
            if(_attack_cds[selectedElement.Value] != 0){
                attack.SetText(_attack_cds[selectedElement.Value].ToString());
            }else attack.SetText("");

            if(_other_cds[selectedElement.Value] != 0){
                other.SetText(_other_cds[selectedElement.Value].ToString());
            } else other.SetText("");   */ 
    }

    //As the name suggest, starts the cooldown of the element in question
    public void StartCooldown(int element, float cd, bool type){
        //type true = attack, type false = other
        if(type){
            switch(element){
                case 0: if(_air_1_cd == 0){
                            _air_1_cd = cd;
                            InvokeRepeating("AirAttackCD", 1.0f, 1.0f);
                        }   
                break;
                case 1: if(_earth_1_cd == 0){
                            _earth_1_cd = cd;
                            InvokeRepeating("EarthAttackCD", 1.0f, 1.0f);
                        }
                break;
                case 2: if(_fire_1_cd == 0){
                            _fire_1_cd = cd;
                            InvokeRepeating("FireAttackCD", 1.0f, 1.0f);
                        }
                break;
                case 3: if(_water_1_cd == 0){
                            _water_1_cd = cd;
                            InvokeRepeating("WaterAttackCD", 1.0f, 1.0f);
                        }
                break;
            }
        }else{
            switch(element){
                case 0: if(_air_2_cd == 0){
                            _air_2_cd = cd;
                            InvokeRepeating("AirOtherCD", 1.0f, 1.0f);
                        }
                break;
                case 1: if(_earth_2_cd == 0){
                            _earth_2_cd = cd;
                            InvokeRepeating("EarthOtherCD", 1.0f, 1.0f);
                        }
                break;
                case 2: if(_fire_2_cd == 0){
                            _fire_2_cd = cd;
                            InvokeRepeating("FireOtherCD", 1.0f, 1.0f);
                        }
                break;
                case 3: if(_water_2_cd == 0){
                            _water_2_cd = cd;
                            InvokeRepeating("WaterOtherCD", 1.0f, 1.0f);
                        }
                break;
            }
        }
    }

    //Invoked functions acting as cooldowns
    void AirAttackCD(){ _air_1_cd--; }

    void AirOtherCD(){ _air_2_cd--; }

    void EarthAttackCD(){ _earth_1_cd--; }

    void EarthOtherCD(){ _earth_2_cd--; }

    void FireAttackCD(){ _fire_1_cd--; }

    void FireOtherCD(){ _fire_2_cd--; }

    void WaterAttackCD(){ _water_1_cd--; }

    void WaterOtherCD(){ _water_2_cd--; }
}
