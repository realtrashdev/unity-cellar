**CREATING EVENT CHANNELS**

1. Make a new C# script and call it <type> + EventChannel

2. Create a struct (outside of the class you just made) and call it <type> + Event

3. Declare struct as [System.Serializeable]

4. Change <type> + EventChannel.cs inheretence from MonoBehavior to GenericScriptableObject<your struct>

5. Create new C# script and call it <type> + EventListener

6. Change inheretence from MonoBehavior to GenericEventListener<[your <type> + EventChannel], [your struct]>