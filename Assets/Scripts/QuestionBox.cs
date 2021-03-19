using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    // Helper struct arg

    // Members
    private QuestionTile[] m_QuestionTiles;

    private void Awake()
    {
        m_QuestionTiles = GetComponentsInChildren<QuestionTile>();
    }

    // Interface

    public uint[] Init(uint num1, uint num2, uint blanks)
    {
        // Split value into components
        uint num1_ones = num1 % 10;
        uint num1_tens = num1 % 100 - num1_ones;
        uint num1_hunds = num1 % 1000 - num1_tens - num1_ones;
        uint num2_ones = num2 % 10;
        uint num2_tens = num2 % 100 - num2_ones;
        uint num2_hunds = num2 % 1000 - num2_tens - num2_ones;

        // Calculate all multiplication values
        uint mult_1_1 = (num1_hunds == 0 ? num1_tens : num1_hunds) * (num2_hunds == 0 ? num2_tens : num2_hunds);
        uint mult_1_2 = (num1_hunds == 0 ? num1_ones : num1_tens) * (num2_hunds == 0 ? num2_tens : num2_hunds);
        uint mult_1_3 = (num1_hunds == 0 ? 0 : num1_ones) * (num2_hunds == 0 ? num2_tens : num2_hunds);
        uint mult_2_1 = (num1_hunds == 0 ? num1_tens : num1_hunds) * (num2_hunds == 0 ? num2_ones : num2_tens);
        uint mult_2_2 = (num1_hunds == 0 ? num1_ones : num1_tens) * (num2_hunds == 0 ? num2_ones : num2_tens);
        uint mult_2_3 = (num1_hunds == 0 ? 0 : num1_ones) * (num2_hunds == 0 ? num2_ones : num2_tens);
        uint mult_3_1 = (num1_hunds == 0 ? num1_tens : num1_hunds) * (num2_hunds == 0 ? 0 : num2_ones);
        uint mult_3_2 = (num1_hunds == 0 ? num1_ones : num1_tens) * (num2_hunds == 0 ? 0 : num2_ones);
        uint mult_3_3 = (num1_hunds == 0 ? 0 : num1_ones) * (num2_hunds == 0 ? 0 : num2_ones);

        // Init list
        List<QuestionTile> tileList = new List<QuestionTile>();

        // Determine which tiles to utilize, and set them all
        foreach (QuestionTile tile in m_QuestionTiles)
        {
            switch (tile.X_Index)
            {
                case 0:
                    switch (tile.Y_Index)
                    {
                        case 0:
                            // Should not exist at all!
                            break;
                        case 1:
                            // num1 components
                            tile.Init(num1_hunds == 0 ? num1_tens : num1_hunds, true);
                            break;
                        case 2:
                            // num1 components
                            tile.Init(num1_hunds == 0 ? num1_ones : num1_tens, true);
                            break;
                        case 3:
                            // num1 components
                            // Does not exist if num1 is 2 digits
                            tile.Init(num1_hunds == 0 ? 0 : num1_ones, true);
                            tile.Show(num1_hunds != 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (tile.Y_Index)
                    {
                        case 0:
                            // num2 component
                            tile.Init(num2_hunds == 0 ? num2_tens : num2_hunds, true);
                            break;
                        case 1:
                            // Mult value
                            tile.Init(mult_1_1, true);
                            break;
                        case 2:
                            // Mult value
                            tile.Init(mult_1_2, true);
                            break;
                        case 3:
                            // Mult value
                            // Does not exist if num1 is 2 digits
                            tile.Init(mult_1_3, true);
                            tile.Show(num1_hunds != 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (tile.Y_Index)
                    {
                        case 0:
                            // num2 component
                            tile.Init(num2_hunds == 0 ? num2_ones : num2_tens, true);
                            break;
                        case 1:
                            // Mult value
                            tile.Init(mult_2_1, true);
                            break;
                        case 2:
                            // Mult value
                            tile.Init(mult_2_2, true);
                            break;
                        case 3:
                            // Mult value
                            // Does not exist if num1 is 2 digits
                            tile.Init(mult_2_3, true);
                            tile.Show(num1_hunds != 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (tile.Y_Index)
                    {
                        case 0:
                            // num2 component
                            // Does not exist if num2 is 2 digits
                            tile.Init(num2_hunds == 0 ? 0 : num2_ones, true);
                            tile.Show(num2_hunds != 0);
                            break;
                        case 1:
                            // Mult value
                            // Does not exist if num2 is 2 digits
                            tile.Init(mult_3_1, true);
                            tile.Show(num2_hunds != 0);
                            break;
                        case 2:
                            // Mult value
                            // Does not exist if num2 is 2 digits
                            tile.Init(mult_3_2, true);
                            tile.Show(num2_hunds != 0);
                            break;
                        case 3:
                            // Mult value
                            // Does not exist if num1 OR num2 is 2 digits
                            tile.Init(mult_3_3, true);
                            tile.Show(num1_hunds != 0 && num2_hunds != 0);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            // Determine if this value is in range
            if (tile.IsShown())
                tileList.Add(tile);
        }

        // Finally, return n values from active tiles
        uint[] draggableValues = new uint[blanks];

        System.Random rand = new System.Random();

        uint index = 0;
        foreach (QuestionTile tile in tileList.OrderBy(x => rand.Next()).Take((int)blanks))
        {
            draggableValues[index++] = tile.GetValue();
            tile.Init(0, false);
        }

        return draggableValues;
    }

    public void ResetInstance()
    {
        foreach (QuestionTile qt in m_QuestionTiles)
            qt.ResetInstance();
    }

    public bool IsCorrect()
    {
        foreach (QuestionTile qt in m_QuestionTiles)
        {
            if (!qt.IsCorrect())
                return false;
        }

        return true;
    }

}
