using GleyInternetAvailability;
using UnityEngine;
using UnityEngine.UI;

public class InternetAvailabilityTest : MonoBehaviour
{
    //make public references for text field
    public Text result;
    public Text message;

    //create public method for button
    public void CheckConnection()
    {
        GleyInternetAvailability.Network.IsAvailable(CompleteMethod);
    }

    //this method will be automatically called when check is complete
    private void CompleteMethod(ConnectionResult connectionResult)
    {
        //if connection result is "Working" user has Internet access not just his card enabled
        if(connectionResult == ConnectionResult.Working)
        {
            result.text = "Network connection is available";
        }
        else
        {
            result.text = "Network is not reachable";
        }

        //this will tell you more details about the connection
        message.text = connectionResult.ToString();
    }
}
