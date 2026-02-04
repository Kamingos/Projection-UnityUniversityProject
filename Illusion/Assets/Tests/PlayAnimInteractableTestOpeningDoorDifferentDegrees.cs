using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Scripts.Tests.TriggerTest;
using NUnit.Framework;
public class PlayAnimInteractableTestOpeningDoorDifferentDegrees
{
    [UnityTest]
    public IEnumerator PlayAnimInteractableTestOpeningDoorDifferentDegreesWithEnumeratorPasses()
    {
        string sceneName = "Assets/Tests/TriggetTest/TriggerTest.unity";

        // загружаем сцену
        yield return SceneManager.LoadSceneAsync(sceneName);


        // запускаем симул€цию
        TriggerTestController controller = GameObject.Find("Controller").GetComponent<TriggerTestController>();

        Quaternion srartDoorTransform = controller.DoorTransform.rotation;

        yield return new WaitForSeconds(1);
        controller.StartSimulation();

        yield return new WaitForSeconds(2);

        Quaternion newDoorTransform = controller.DoorTransform.rotation;


        // свер€ем
        Assert.AreNotEqual(srartDoorTransform, newDoorTransform);
    }
}
