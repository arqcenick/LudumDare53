using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager
{
    public static OrderManager Instance;

    public static OrderData CreateNewOrder(int difficulty)
    {

        OrderData orderData = new OrderData();


        //difficulty / 2 determines order size
        //dificulty % 2 determines order complexity

        int size = difficulty / 3 + 1;
        int complexity = difficulty % 3;


        List<Cargo.CargoType> cargoTypes = new List<Cargo.CargoType>();
        List<Cargo.CargoType> possibleColors = new List<Cargo.CargoType>();
        for (int i = 0; i < 3; i++)
        {
            possibleColors.Add((Cargo.CargoType)i);
        }
        if (complexity == 0)
        {
            possibleColors.RemoveAt(Random.Range(0, 3));
            possibleColors.RemoveAt(Random.Range(0, 2));

        }
        else if (complexity == 1)
        {
            possibleColors.RemoveAt(Random.Range(0, 3));
        }
        for (int i = 0; i < size; i++)
        {



            Cargo.CargoType chosenColor = possibleColors[Random.Range(0, possibleColors.Count)];


            cargoTypes.Add(chosenColor);
        }
        orderData.TimeLimit = size * 10 + 5;
        orderData.CargoTypes = cargoTypes;

        return orderData;
    }

    public struct OrderData
    {
        public List<Cargo.CargoType> CargoTypes;
        public float TimeLimit;
    }
}
