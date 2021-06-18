using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class DamageModificator{
    protected SortedList<int,int> flatModificator = new SortedList<int,int>();
    protected SortedList<int,float> percentualModificator = new SortedList<int,float>();

    public DamageModificator(){
        flatModificator[0] = 0;
        percentualModificator[0] = 0;
    }
    public void ChangeFlatModificator(int level, int value){
        if (flatModificator.ContainsKey(level))
        {
            flatModificator[level] += value;
        }
         else
        {
            for (var i = 0; i < level; i++)
            {
                if (!flatModificator.ContainsKey(i))
                {
                    flatModificator[i] = 0;
                }       
            }
            flatModificator[level] = value;
        }
    }
    public void ChangePercentualModificator(int level, float value){
        if (percentualModificator.ContainsKey(level))
        {
            percentualModificator[level] += value;
        }
         else
        {
            for (var i = 0; i < level; i++)
            {
                if (!percentualModificator.ContainsKey(i))
                {
                    percentualModificator[i] = 0;
                }       
            }
            percentualModificator[level] = value;
        }
    }
}
public class DefenceModificator : DamageModificator{
    public DefenceModificator(){
        flatModificator[0] = 0;
        percentualModificator[0] = 0;
    }
    public int DamageAfterDefences(int incomingDamage){
        int loopsPercentual = percentualModificator.Count;
        int loopsFlat = flatModificator.Count;
        int loops = Math.Max(loopsFlat,loopsPercentual);

        for (var i = 0; i < loops; i++)
        {
            if (flatModificator.ContainsKey(i)){
                incomingDamage -= flatModificator[i];
            }
            if(percentualModificator.ContainsKey(i)){
                incomingDamage = (int)((1-percentualModificator[i])*incomingDamage);
            }
        }
        return incomingDamage;
    }
}
public class OffenseModificator : DamageModificator{
    public OffenseModificator(){
        flatModificator[0] = 0;
        percentualModificator[0] = 0;
    }
    public int DamageAfterOffenses(int incomingDamage){
        int loopsPercentual = percentualModificator.Count;
        int loopsFlat = flatModificator.Count;
        int loops = Math.Max(loopsFlat,loopsPercentual);

        for (var i = 0; i < loops; i++)
        {
            if (flatModificator.ContainsKey(i)){
                incomingDamage += flatModificator[i];
            }
            if(percentualModificator.ContainsKey(i)){
                incomingDamage = (int)((1+percentualModificator[i])*incomingDamage);
            }
        }
        return incomingDamage;
    }
}