using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BM3GenericDataParser <T>  where T : new()
{
    virtual protected T ParseItem(List<string> itemParameters)
    {
        return new T();
    }

    /// <summary>
    /// Parses all items in a textAssets, using the rules defined in this implementation of the DataParser
    /// </summary>
    /// <returns>The all items.</returns>
    /// <param name="textAsset">Text asset.</param>
    public List<T> ParseAllItems(TextAsset textAsset)
    {
        string[,] grid = CSVParser.SplitCsvGrid(textAsset.text);
        List<T> items = new List<T>();

        //We iterate through our grid, select a row, and parse one single item with each row.
        for(int i = 0; i < grid.GetUpperBound(1); i++)
        {
            List<string> itemsInRow = new List<string>();
            for(int j = 0; j < grid.GetUpperBound(0); j++)
            {
                if(grid[j,i] != null)
                {
                   itemsInRow.Add(grid[j,i]);
                }
            }
            T currentItem = this.ParseItem( itemsInRow);
            if( currentItem != null)
                items.Add(currentItem);
        }

        return items;
    }
}

