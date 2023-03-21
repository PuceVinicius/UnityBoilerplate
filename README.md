# UnityBoilerplate
Unity Boilerplate project with generic and necessary systems that most games will require.

From 3D HDRP template, Unity 2021.3.8f1

# To Do

## Channels

Based on [Chop Chop](https://github.com/UnityTechnologies/open-project-1) implemented script communication, it allows communication between assemblies without hard reference and enables a modular system setup

### FuncChannel: 
1. FuncUtils.CallDelegate() overload with one additional parameter: a return value on error. In this case, if the CallDelegate() method catches an error, this parameter will set which value will be returned, instead of the type's default return value.
2. All FuncChannels inherit from the same abstract class FuncChannel, instead of DescriptionScriptableObject.
3. Add template script for creating new FuncChannels.
4. Try to replace FuncUtils public static methods with operator overloads.

### EventChannel: 
1. Remove EventUtils methods from stacktrace when the EventUtils catches an error, already implemented on FuncUtils
2. All EventChannels inherit from the same abstract class EventChannel, instead of DescriptionScriptableObject.
3. Add bool parameter "can EventChannel be null"
4. Add template script for creating new EventChannels.
5. Try to replace EventUtils public static methods with operator overloads.
6. Add error messages to const variables
