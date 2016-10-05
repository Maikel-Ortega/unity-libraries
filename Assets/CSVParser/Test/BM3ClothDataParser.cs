using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BM3ClothDataParser : BM3GenericDataParser<TestClothData> 
{
    public string forbiddenValue = "RAREZA";


    protected override TestClothData ParseItem(List<string> itemParameters)
    {
        //Añadir algun valor de la cabecera usada en el csv para eliminar filas completamente
        if(itemParameters.Contains(forbiddenValue))
            return null;    

        //En nuestro caso, testClothData sabemos que tiene solo 3 strings, y los metemos tal cual. 
        TestClothData toReturn = new TestClothData(itemParameters[0],itemParameters[1],itemParameters[2]);
        return toReturn;
    }
}
