#region// RESTRICTIONS ON DYNAMIC ASSEMBLY SCENARIOS
// .NET Framework 4.5  
// Other Versions  
// This topic has not yet been rated - Rate this topic  
//Reflection emit supports the creation of dynamic modules defined in dynamic assemblies. A dynamic module created in a dynamic assembly can be transient or persistable.
//Restrictions on Type References 
//--------------------------------------------------------------------------------
//Assemblies can reference types defined in another assembly. A transient dynamic assembly can safely reference types defined in another transient dynamic assembly, a persistable dynamic assembly, or a static assembly. However, the common language runtime does not allow a persistable dynamic module to reference a type defined in a transient dynamic module. This is because when the persisted dynamic module is loaded after being saved to disk, the runtime cannot resolve the references to types defined in the transient dynamic module.
//Restrictions on Emitting to Remote Application Domains 
//--------------------------------------------------------------------------------
//Some scenarios require a dynamic assembly to be created and executed in a remote application domain. Reflection emit does not allow a dynamic assembly to be emitted directly to a remote application domain. The solution is to emit the dynamic assembly in the current application domain, save the emitted dynamic assembly to disk, and then load the dynamic assembly into the remote application domain.
//Dynamic Assembly Access Modes 
//--------------------------------------------------------------------------------
//Dynamic assemblies can be created using one of the following access modes: 
//•
//Run 
//The dynamic assembly represented by an AssemblyBuilder is transient. The dynamic assembly can only be used to execute the emitted code. 
//•
//Save 
//The dynamic assembly represented by an AssemblyBuilder is persistable but cannot be executed until the saved portable executable (PE) file has been loaded from disk. 
//•
//RunAndSave 
//The dynamic assembly represented by an AssemblyBuilder is persistable but can also be executed before and/or after the assembly is saved to disk. 
//The access mode must be specified when the dynamic assembly is defined and cannot be changed later. The runtime uses the access mode of a dynamic assembly to optimize the assembly's internal representation.
#endregion
//
#region// RESTRICTIONS ON COLLECTIONS
//--------------------------------------------------------------------------------
//The following restrictions apply to collectible assemblies: 
//•
//Static references   Types in an ordinary dynamic assembly cannot have static references to types that are defined in a collectible assembly. For example, if you define an ordinary type that inherits a type in a collectible assembly, a NotSupportedException exception is thrown. A type in a collectible assembly can have static references to a type in another collectible assembly, but this extends the lifetime of the referenced assembly to the lifetime of the referencing assembly.
//•
//COM interop   No COM interfaces can be defined within a collectible assembly, and no instances of types within a collectible assembly can be converted into COM objects. A type in a collectible assembly cannot serve as a COM callable wrapper (CCW) or runtime callable wrapper (RCW). However, types in collectible assemblies can use objects that implement COM interfaces.
//•
//Platform invoke   Methods that have the DllImportAttribute attribute will not compile when they are declared within a collectible assembly. The OpCodesCalli instruction cannot be used in the implementation of a type in a collectible assembly, and such types cannot be marshaled to unmanaged code. However, you can call into native code by using an entry point that is declared in a non-collectible assembly. 
//•
//Marshaling   Objects that are defined in collectible assemblies (in particular, delegates) cannot be marshaled. This is a restriction on all transient emitted types.
//•
//Assembly loading   Reflection emit is the only mechanism that is supported for loading collectible assemblies. Assemblies that are loaded by any other form of assembly loading cannot be unloaded.
//•
//Context-bound objects   Context-static variables are not supported. Types in a collectible assembly cannot extend ContextBoundObject. However, code in collectible assemblies can use context-bound objects that are defined elsewhere.
//•
//Thread-static data   Thread-static variables are not supported.
//--------------------------------------------------------------------------------
#endregion
#region// Using Reflection to get information from an Assembly:
//System.Reflection.Assembly info = typeof(System.Int32).Assembly;
//System.Console.WriteLine(info);
// Using GetType to obtain type information: 
//int i = 42;
//System.Type type = i.GetType();
//System.Console.WriteLine(type);
//The output is:
//mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 
//The C# keywords protected and internal have no meaning in IL and are not used in the reflection APIs. The corresponding terms in IL are Family and Assembly. To identify an internal method using reflection, use the IsAssembly property. To identify a protected internal method, use the IsFamilyOrAssembly.
//
//--------------------------------------------------------------------------------
#endregion
#region// Reflection is useful in the following situations:
//•
//When you have to access attributes in your program's metadata. For more information, see Retrieving Information Stored in Attributes.
//•
//For examining and instantiating types in an assembly.
//•
//For building new types at runtime. Use classes in System.Reflection.Emit.
//•
//For performing late binding, accessing methods on types created at run time. See the topic Dynamically Loading and Using Types.
 //Type t = typeof(CustomBinderDriver);
 //       CustomBinder binder = new CustomBinder();
 //       BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Instance |
 //           BindingFlags.Public | BindingFlags.Static;
 //       object[] args;
 //       // Case 1. Neither argument coercion nor member selection is needed.
 //       args = new object[] {};
 //       t.InvokeMember("PrintBob", flags, binder, null, args);
 //       // Case 2. Only member selection is needed.
 //       args = new object[] {42};
 //       t.InvokeMember("PrintValue", flags, binder, null, args);
 //       // Case 3. Only argument coercion is needed.
 //       args = new object[] {"5.5"};
 //       t.InvokeMember("PrintNumber", flags, binder, null, args);
 //   }
//Overload resolution is needed when more than one member with the same name is available. The BinderBindToMethod and BinderBindToField methods are used to resolve binding to a single member. Binder.BindToMethod also provides property resolution through the get and set property accessors.
//BindToMethod returns the MethodBase to invoke, or a null reference (Nothing in Visual Basic) if no such invocation is possible. The MethodBase return value need not be one of those contained in the match parameter, although that is the usual case.
//When ByRef arguments are present, the caller might want to get them back. Therefore, Binder allows a client to map the array of arguments back to its original form if BindToMethod has manipulated the argument array. In order to do this, the caller must be guaranteed that the order of the arguments is unchanged. When arguments are passed by name, Binder reorders the argument array, and that is what the caller sees. For more information, see BinderReorderArgumentArray.
//The set of available members are those members defined in the type or any base type. If BindingFlags.NonPublic is specified, members of any accessibility will be returned in the set. If BindingFlags.NonPublic is not specified, the binder must enforce accessibility rules. When specifying the Public or NonPublic binding flag, you must also specify the Instance or Static binding flag, or no members will be returned.
//If there is only one member of the given name, no callback is necessary, and binding is done on that method. Case 1 of the code example illustrates this point: Only one PrintBob method is available, and therefore no callback is needed.
//If there is more than one member in the available set, all these methods are passed to BindToMethod, which selects the appropriate method and returns it. In Case 2 of the code example, there are two methods named PrintValue. The appropriate method is selected by the call to BindToMethod.
//ChangeType performs argument coercion (type conversion), which converts the actual arguments to the type of the formal arguments of the selected method. ChangeType is called for every argument even if the types match exactly.
//In Case 3 of the code example, an actual argument of type String with a value of "5.5" is passed to a method with a formal argument of type Double. For the invocation to succeed, the string value "5.5" must be converted to a double value. ChangeType performs this conversion.
//ChangeType performs only lossless or widening coercions, as shown in the following table.
//
//Source type
//Target type
//
//Any type
//Its base type
//
//Any type
//Interface it implements
//
//Char
//UInt16, UInt32, Int32, UInt64, Int64, Single, Double
//
//Byte
//Char, UInt16, Int16, UInt32, Int32, UInt64, Int64, Single, Double
//
//SByte
//Int16, Int32, Int64, Single, Double
//
//UInt16
//UInt32, Int32, UInt64, Int64, Single, Double
//
//Int16
//Int32, Int64, Single, Double
//
//UInt32
//UInt64, Int64, Single, Double
//
//Int32
//Int64, Single, Double
//
//UInt64
//Single, Double
//
//Int64
//Single, Double
//
//Single
//Double
//
//Nonreference type
//Reference type
//
//The Type class has Get methods that use parameters of type Binder to resolve references to a particular member. TypeGetConstructor, TypeGetMethod, and TypeGetProperty search for a particular member of the current type by providing signature information for that member. BinderSelectMethod and BinderSelectProperty are called back on to select the given signature information of the appropriate methods.
//•
//Enumerate types and members, and examine their metadata.
//•
//Enumerate and examine assemblies and modules.
//Using reflection to access members, by contrast, is subject to restrictions. Beginning with the .NET Framework 4, only trusted code can use reflection to access security-critical members. Furthermore, only trusted code can use reflection to access nonpublic members that would not be directly accessible to compiled code. Finally, code that uses reflection to access a safe-critical member must have whatever permissions the safe-critical member demands, just as with compiled code.
//Subject to necessary permissions, code can use reflection to perform the following kinds of access:
//•
//Access public members that are not security-critical.
//•
//Access nonpublic members that would be accessible to compiled code, if those members are not security-critical. Examples of such nonpublic members include:
//◦
//Protected members of the calling code's base classes. (In reflection, this is referred to as family-level access.)
//◦
//internal  members (Friend members in Visual Basic) in the calling code's assembly. (In reflection, this is referred to as assembly-level access.)
//◦
//Private members of other instances of the class that contains the calling code.
//Reflection emit provides the following capabilities: 
//•
//Define lightweight global methods at run time, using the DynamicMethod class, and execute them using delegates.
//•
//Define assemblies at run time and then run them and/or save them to disk.
//•
//Define assemblies at run time, run them, and then unload them and allow garbage collection to reclaim their resources.
//•
//Define modules in new assemblies at run time and then run and/or save them to disk.
//•
//Define types in modules at run time, create instances of these types, and invoke their methods.
//•
//Define symbolic information for defined modules that can be used by tools such as debuggers and code profilers.
//In addition to the managed types in the System.Reflection.Emit namespace, there are unmanaged metadata interfaces which are described in the Metadata Interfaces reference documentation. Managed reflection emit provides stronger semantic error checking and a higher level of abstraction of the metadata than the unmanaged metadata interfaces. 
//Another useful resource for working with metadata and MSIL is the Common Language Infrastructure (CLI) documentation, especially "Partition II: Metadata Definition and Semantics" and "Partition III: CIL Instruction Set". The documentation is available online on MSDN and at the Ecma Web site.
//
//
#endregion
#region// HOOKING UP DELATGATE
//1.Load an assembly that contains a type that raises events. Assemblies are usually loaded with the AssemblyLoad method. To keep this example simple, a derived form in the current assembly is used, so the GetExecutingAssembly method is used to load the current assembly.
//Assembly assem = Assembly.GetExecutingAssembly();
//2.Get a Type object representing the type, and create an instance of the type. The CreateInstance(Type) method is used in the following code because the form has a default constructor. There are several other overloads of the CreateInstance method that you can use if the type you are creating does not have a default constructor. The new instance is stored as type Object to maintain the fiction that nothing is known about the assembly. (Reflection allows you to get the types in an assembly without knowing their names in advance.)
//Type tExForm = assem.GetType("ExampleForm");
//Object exFormAsObj = Activator.CreateInstance(tExForm);
//3.Get an EventInfo object representing the event, and use the EventHandlerType property to get the type of delegate used to handle the event. In the following code, an EventInfo for the Click event is obtained. 
//EventInfo evClick = tExForm.GetEvent("Click");
//Type tDelegate = evClick.EventHandlerType;
//4.Get a MethodInfo object representing the method that handles the event. The complete program code in the Example section later in this topic contains a method that matches the signature of the EventHandler delegate, which handles the Click event, but you can also generate dynamic methods at run time. For details, see the accompanying procedure To Generate an Event Handler at Run Time by Using a Dynamic Method. 
//MethodInfo miHandler = 
//    typeof(Example).GetMethod("LuckyHandler", 
//        BindingFlags.NonPublic | BindingFlags.Instance);
//Delegate d = Delegate.CreateDelegate(tDelegate, this, miHandler);
//5.Create an instance of the delegate, using the CreateDelegate method. This method is static (Shared in Visual Basic), so the delegate type must be supplied. Using the overloads of CreateDelegate that take a MethodInfo is recommended.
//6.Get the add accessor method and invoke it to hook up the event. All events have an add accessor and a remove accessor, which are hidden by the syntax of high-level languages. For example, C# uses the += operator to hook up events, and Visual Basic uses the AddHandler statement. The following code gets the add accessor of the Click event and invokes it late-bound, passing in the delegate instance. The arguments must be passed as an array.
//MethodInfo addHandler = evClick.GetAddMethod();
//Object[] addHandlerArgs = { d };
//addHandler.Invoke(exFormAsObj, addHandlerArgs);
//MethodInfo addHandler = evClick.GetAddMethod();
//Object[] addHandlerArgs = { d };
//addHandler.Invoke(exFormAsObj, addHandlerArgs);
//Application.Run((Form) exFormAsObj);
#endregion
#region// To generate an event handler at run time by using a dynamic method
//1.
//Event-handler methods can be generated at run time, using lightweight dynamic methods and reflection emit. To construct an event handler, you need the return type and parameter types of the delegate. These can be obtained by examining the delegate's Invoke method. The following code uses the GetDelegateReturnType and GetDelegateParameterTypes methods to obtain this information. The code for these methods can be found in the Example section later in this topic.
//It is not necessary to name a DynamicMethod, so the empty string can be used. In the following code, the last argument associates the dynamic method with the current type, giving the delegate access to all the public and private members of the Example class.
//C#
//C++
//VB
//Copy
//Type returnType = GetDelegateReturnType(tDelegate);
//if (returnType != typeof(void))
//    throw new ApplicationException("Delegate has a return type.");
//DynamicMethod handler = 
//    new DynamicMethod("", 
//                      null,
//                      GetDelegateParameterTypes(tDelegate),
//                      typeof(Example));
//2.
//Generate a method body. This method loads a string, calls the overload of the MessageBoxShow method that takes a string, pops the return value off the stack (because the handler has no return type), and returns. To learn more about emitting dynamic methods, see How to: Define and Execute Dynamic Methods.
//C#
//C++
//VB
//Copy
//ILGenerator ilgen = handler.GetILGenerator();
//Type[] showParameters = { typeof(String) };
//MethodInfo simpleShow = 
//    typeof(MessageBox).GetMethod("Show", showParameters);
//ilgen.Emit(OpCodes.Ldstr, 
//    "This event handler was constructed at run time.");
//ilgen.Emit(OpCodes.Call, simpleShow);
//ilgen.Emit(OpCodes.Pop);
//ilgen.Emit(OpCodes.Ret);
//3.
//Complete the dynamic method by calling its CreateDelegate method. Use the add accessor to add the delegate to the invocation list for the event.
//C#
//C++
//VB
//Copy
//Delegate dEmitted = handler.CreateDelegate(tDelegate);
//addHandler.Invoke(exFormAsObj, new Object[] { dEmitted });
//4.
//Test the event. The following code loads the form defined in the code example. Clicking the form invokes both the predefined event handler and the emitted event handler.
//C#
//C++
//VB
//Copy
//Application.Run((Form) exFormAsObj);
//Example 
//--------------------------------------------------------------------------------
//The following code example shows how to hook up an existing method to an event using reflection, and also how to use the DynamicMethod class to emit a method at run time and hook it up to an event.
//C#
//C++
//VB
//using System;
//using System.Reflection;
//using System.Reflection.Emit;
//using System.Windows.Forms;
//class ExampleForm : Form 
//{
//    public ExampleForm() : base()
//    {
//        this.Text = "Click me";
//    }
//}
//class Example
//{
//    public static void Main()
//    {
//        Example ex = new Example();
//        ex.HookUpDelegate();
//    }
//    private void HookUpDelegate()
//    {
//        // Load an assembly, for example using the Assembly.Load 
//        // method. In this case, the executing assembly is loaded, to 
//        // keep the demonstration simple. 
//        //
//        Assembly assem = Assembly.GetExecutingAssembly();
//        // Get the type that is to be loaded, and create an instance  
//        // of it. Activator.CreateInstance has other overloads, if 
//        // the type lacks a default constructor. The new instance 
//        // is stored as type Object, to maintain the fiction that  
//        // nothing is known about the assembly. (Note that you can 
//        // get the types in an assembly without knowing their names 
//        // in advance.) 
//        //
//        Type tExForm = assem.GetType("ExampleForm");
//        Object exFormAsObj = Activator.CreateInstance(tExForm);
//        // Get an EventInfo representing the Click event, and get the 
//        // type of delegate that handles the event. 
//        //
//        EventInfo evClick = tExForm.GetEvent("Click");
//        Type tDelegate = evClick.EventHandlerType;
//        // If you already have a method with the correct signature, 
//        // you can simply get a MethodInfo for it.  
//        //
//        MethodInfo miHandler = 
//            typeof(Example).GetMethod("LuckyHandler", 
//                BindingFlags.NonPublic | BindingFlags.Instance);
//        // Create an instance of the delegate. Using the overloads 
//        // of CreateDelegate that take MethodInfo is recommended. 
//        //
//        Delegate d = Delegate.CreateDelegate(tDelegate, this, miHandler);
//        // Get the "add" accessor of the event and invoke it late-
//        // bound, passing in the delegate instance. This is equivalent 
//        // to using the += operator in C#, or AddHandler in Visual 
//        // Basic. The instance on which the "add" accessor is invoked
//        // is the form; the arguments must be passed as an array. 
//        //
//        MethodInfo addHandler = evClick.GetAddMethod();
//        Object[] addHandlerArgs = { d };
//        addHandler.Invoke(exFormAsObj, addHandlerArgs);
//        // Event handler methods can also be generated at run time, 
//        // using lightweight dynamic methods and Reflection.Emit.  
//        // To construct an event handler, you need the return type 
//        // and parameter types of the delegate. These can be obtained 
//        // by examining the delegate's Invoke method.  
//        // 
//        // It is not necessary to name dynamic methods, so the empty  
//        // string can be used. The last argument associates the  
//        // dynamic method with the current type, giving the delegate 
//        // access to all the public and private members of Example, 
//        // as if it were an instance method. 
//        //
//        Type returnType = GetDelegateReturnType(tDelegate);
//        if (returnType != typeof(void))
//            throw new ApplicationException("Delegate has a return type.");
//        DynamicMethod handler = 
//            new DynamicMethod("", 
//                              null,
//                              GetDelegateParameterTypes(tDelegate),
//                              typeof(Example));
//        // Generate a method body. This method loads a string, calls  
//        // the Show method overload that takes a string, pops the  
//        // return value off the stack (because the handler has no 
//        // return type), and returns. 
//        //
//        ILGenerator ilgen = handler.GetILGenerator();
//        Type[] showParameters = { typeof(String) };
//        MethodInfo simpleShow = 
//            typeof(MessageBox).GetMethod("Show", showParameters);
//        ilgen.Emit(OpCodes.Ldstr, 
//            "This event handler was constructed at run time.");
//        ilgen.Emit(OpCodes.Call, simpleShow);
//        ilgen.Emit(OpCodes.Pop);
//        ilgen.Emit(OpCodes.Ret);
//        // Complete the dynamic method by calling its CreateDelegate 
//        // method. Use the "add" accessor to add the delegate to
//        // the invocation list for the event. 
//        //
//        Delegate dEmitted = handler.CreateDelegate(tDelegate);
//        addHandler.Invoke(exFormAsObj, new Object[] { dEmitted });
//        // Show the form. Clicking on the form causes the two 
//        // delegates to be invoked. 
//        //
//        Application.Run((Form) exFormAsObj);
//    }
//    private void LuckyHandler(Object sender, EventArgs e)
//    {
//        MessageBox.Show("This event handler just happened to be lying around.");
//    }
//    private Type[] GetDelegateParameterTypes(Type d)
//    {
//        if (d.BaseType != typeof(MulticastDelegate))
//            throw new ApplicationException("Not a delegate.");
//        MethodInfo invoke = d.GetMethod("Invoke");
//        if (invoke == null)
//            throw new ApplicationException("Not a delegate.");
//        ParameterInfo[] parameters = invoke.GetParameters();
//        Type[] typeParameters = new Type[parameters.Length];
//        for (int i = 0; i < parameters.Length; i++)
//        {
//            typeParameters[i] = parameters[i].ParameterType;
//        }
//        return typeParameters;
//    }
//    private Type GetDelegateReturnType(Type d)
//    {
//        if (d.BaseType != typeof(MulticastDelegate))
//            throw new ApplicationException("Not a delegate.");
//        MethodInfo invoke = d.GetMethod("Invoke");
//        if (invoke == null)
//            throw new ApplicationException("Not a delegate.");
//        return invoke.ReturnType;
//    }//}
#endregion
////
#region// Dynamic Method Binding
//The following procedures show how to define and execute a simple dynamic method and a dynamic method bound to an instance of a class. For more information on dynamic methods, see the DynamicMethod class and Reflection Emit Dynamic Method Scenarios.
//To define and execute a dynamic method
//1.
//Declare a delegate type to execute the method. Consider using a generic delegate to minimize the number of delegate types you need to declare. The following code declares two delegate types that could be used for the SquareIt method, and one of them is generic.
//C#
//C++
//VB
//Copy
//private delegate long SquareItInvoker(int input);
//private delegate TReturn OneParameter<TReturn, TParameter0>
//    (TParameter0 p0);
//2.
//Create an array that specifies the parameter types for the dynamic method. In this example, the only parameter is an int (Integer in Visual Basic), so the array has only one element.
//C#
//C++
//VB
//Copy
//Type[] methodArgs = {typeof(int)};
//3.
//Create a DynamicMethod. In this example the method is named SquareIt. 
//Note Note 
//It is not necessary to give dynamic methods names, and they cannot be invoked by name. Multiple dynamic methods can have the same name. However, the name appears in call stacks and can be useful for debugging.
//The type of the return value is specified as long. The method is associated with the module that contains the Example class, which contains the example code. Any loaded module could be specified. The dynamic method acts like a module-level static method (Shared in Visual Basic).
//C#
//C++
//VB
//Copy
//DynamicMethod squareIt = new DynamicMethod(
//    "SquareIt", 
//    typeof(long), 
//    methodArgs, 
//    typeof(Example).Module);
//4.
//Emit the method body. In this example, an ILGenerator object is used to emit the Microsoft intermediate language (MSIL). Alternatively, a DynamicILInfo object can be used in conjunction with unmanaged code generators to emit the method body for a DynamicMethod.
//The MSIL in this example loads the argument, which is an int, onto the stack, converts it to a long, duplicates the long, and multiplies the two numbers. This leaves the squared result on the stack, and all the method has to do is return.
//C#
//C++
//VB
//Copy
//ILGenerator il = squareIt.GetILGenerator();
//il.Emit(OpCodes.Ldarg_0);
//il.Emit(OpCodes.Conv_I8);
//il.Emit(OpCodes.Dup);
//il.Emit(OpCodes.Mul);
//il.Emit(OpCodes.Ret);
//5.
//Create an instance of the delegate (declared in step 1) that represents the dynamic method by calling the CreateDelegate method. Creating the delegate completes the method, and any further attempts to change the method — for example, adding more MSIL — are ignored. The following code creates the delegate and invokes it, using a generic delegate.
//C#
//C++
//VB
//Copy
//OneParameter<long, int> invokeSquareIt = 
//    (OneParameter<long, int>)
//    squareIt.CreateDelegate(typeof(OneParameter<long, int>));
//Console.WriteLine("123456789 squared = {0}",
//    invokeSquareIt(123456789));
#endregion
#region// To define and execute a dynamic method that is bound to an object
//1.
//Declare a delegate type to execute the method. Consider using a generic delegate to minimize the number of delegate types you need to declare. The following code declares a generic delegate type that can be used to execute any method with one parameter and a return value, or a method with two parameters and a return value if the delegate is bound to an object.
//C#
//C++
//VB
//Copy
//private delegate TReturn OneParameter<TReturn, TParameter0>
//    (TParameter0 p0);
//2.
//Create an array that specifies the parameter types for the dynamic method. If the delegate representing the method is to be bound to an object, the first parameter must match the type the delegate is bound to. In this example, there are two parameters, of type Example and type int (Integer in Visual Basic).
//C#
//C++
//VB
//Copy
//Type[] methodArgs2 = { typeof(Example), typeof(int) };
//3.
//Create a DynamicMethod. In this example the method has no name. The type of the return value is specified as int (Integer in Visual Basic). The method has access to the private and protected members of the Example class.
//C#
//C++
//VB
//Copy
//DynamicMethod multiplyHidden = new DynamicMethod(
//    "", 
//    typeof(int), 
//    methodArgs2, 
//    typeof(Example));
//4.
//Emit the method body. In this example, an ILGenerator object is used to emit the Microsoft intermediate language (MSIL). Alternatively, a DynamicILInfo object can be used in conjunction with unmanaged code generators to emit the method body for a DynamicMethod.
//The MSIL in this example loads the first argument, which is an instance of the Example class, and uses it to load the value of a private instance field of type int. The second argument is loaded, and the two numbers are multiplied. If the result is larger than int, the value is truncated and the most significant bits are discarded. The method returns, with the return value on the stack.
//C#
//C++
//VB
//Copy
//ILGenerator ilMH = multiplyHidden.GetILGenerator();
//ilMH.Emit(OpCodes.Ldarg_0);
//FieldInfo testInfo = typeof(Example).GetField("test",
//    BindingFlags.NonPublic | BindingFlags.Instance);
//ilMH.Emit(OpCodes.Ldfld, testInfo);
//ilMH.Emit(OpCodes.Ldarg_1);
//ilMH.Emit(OpCodes.Mul);
//ilMH.Emit(OpCodes.Ret);
//5.
//Create an instance of the delegate (declared in step 1) that represents the dynamic method by calling the CreateDelegate(Type, Object) method overload. Creating the delegate completes the method, and any further attempts to change the method — for example, adding more MSIL — are ignored. 
//Note Note 
//You can call the CreateDelegate method multiple times to create delegates bound to other instances of the target type.
//The following code binds the method to a new instance of the Example class whose private test field is set to 42. That is, each time the delegate is invoked the instance of Example is passed to the first parameter of the method.
//The delegate OneParameter is used because the first parameter of the method always receives the instance of Example. When the delegate is invoked, only the second parameter is required.
//C#
//C++
//VB
//Copy
//OneParameter<int, int> invoke = (OneParameter<int, int>)
//    multiplyHidden.CreateDelegate(
//        typeof(OneParameter<int, int>), 
//        new Example(42)
//    );
//Console.WriteLine("3 * test = {0}", invoke(3));
#endregion
#region// Simple Dynamic vs Dynamic Method Bound
//Example 
//--------------------------------------------------------------------------------
//The following code example demonstrates a simple dynamic method and a dynamic method bound to an instance of a class.
//The simple dynamic method takes one argument, a 32-bit integer, and returns the 64-bit square of that integer. A generic delegate is used to invoke the method.
//The second dynamic method has two parameters, of type Example and type int (Integer in Visual Basic). When the dynamic method has been created, it is bound to an instance of Example, using a generic delegate that has one argument of type int. The delegate does not have an argument of type Example because the first parameter of the method always receives the bound instance of Example. When the delegate is invoked, only the int argument is supplied. This dynamic method accesses a private field of the Example class and returns the product of the private field and the int argument.
//The code example defines delegates that can be used to execute the methods. 
//C#
//C++
//VB
//using System;
//using System.Reflection;
//using System.Reflection.Emit;
//public class Example
//{
//    // The following constructor and private field are used to 
//    // demonstrate a method bound to an object. 
//    private int test;
//    public Example(int test) { this.test = test; }
//    // Declare delegates that can be used to execute the completed  
//    // SquareIt dynamic method. The OneParameter delegate can be  
//    // used to execute any method with one parameter and a return 
//    // value, or a method with two parameters and a return value 
//    // if the delegate is bound to an object. 
//    // 
//    private delegate long SquareItInvoker(int input);
//    private delegate TReturn OneParameter<TReturn, TParameter0>
//        (TParameter0 p0);
//    public static void Main()
//    {
//        // Example 1: A simple dynamic method. 
//        // 
//        // Create an array that specifies the parameter types for the 
//        // dynamic method. In this example the only parameter is an  
//        // int, so the array has only one element. 
//        //
//        Type[] methodArgs = {typeof(int)};
//        // Create a DynamicMethod. In this example the method is 
//        // named SquareIt. It is not necessary to give dynamic  
//        // methods names. They cannot be invoked by name, and two 
//        // dynamic methods can have the same name. However, the  
//        // name appears in calls stacks and can be useful for 
//        // debugging.  
//        // 
//        // In this example the return type of the dynamic method 
//        // is long. The method is associated with the module that  
//        // contains the Example class. Any loaded module could be 
//        // specified. The dynamic method is like a module-level 
//        // static method. 
//        //
//        DynamicMethod squareIt = new DynamicMethod(
//            "SquareIt", 
//            typeof(long), 
//            methodArgs, 
//            typeof(Example).Module);
//        // Emit the method body. In this example ILGenerator is used 
//        // to emit the MSIL. DynamicMethod has an associated type 
//        // DynamicILInfo that can be used in conjunction with  
//        // unmanaged code generators. 
//        // 
//        // The MSIL loads the argument, which is an int, onto the  
//        // stack, converts the int to a long, duplicates the top 
//        // item on the stack, and multiplies the top two items on the 
//        // stack. This leaves the squared number on the stack, and  
//        // all the method has to do is return. 
//        //
//        ILGenerator il = squareIt.GetILGenerator();
//        il.Emit(OpCodes.Ldarg_0);
//        il.Emit(OpCodes.Conv_I8);
//        il.Emit(OpCodes.Dup);
//        il.Emit(OpCodes.Mul);
//        il.Emit(OpCodes.Ret);
#endregion
#region// Delegate Representing Dynamic
//        // Create a delegate that represents the dynamic method.  
//        // Creating the delegate completes the method, and any further  
//        // attempts to change the method (for example, by adding more 
//        // MSIL) are ignored. The following code uses a generic  
//        // delegate that can produce delegate types matching any 
//        // single-parameter method that has a return type. 
//        //
//        OneParameter<long, int> invokeSquareIt = 
//            (OneParameter<long, int>)
//            squareIt.CreateDelegate(typeof(OneParameter<long, int>));
//        Console.WriteLine("123456789 squared = {0}",
//            invokeSquareIt(123456789));
#endregion
#region// Dynamic Metho Bound to an Instance
//        // Example 2: A dynamic method bound to an instance. 
//        // 
//        // Create an array that specifies the parameter types for a 
//        // dynamic method. If the delegate representing the method 
//        // is to be bound to an object, the first parameter must  
//        // match the type the delegate is bound to. In the following 
//        // code the bound instance is of the Example class.  
//        //
//        Type[] methodArgs2 = { typeof(Example), typeof(int) };
//        // Create a DynamicMethod. In this example the method has no 
//        // name. The return type of the method is int. The method  
//        // has access to the protected and private data of the  
//        // Example class. 
//        //
//        DynamicMethod multiplyHidden = new DynamicMethod(
//            "", 
//            typeof(int), 
//            methodArgs2, 
//            typeof(Example));
//        // Emit the method body. In this example ILGenerator is used 
//        // to emit the MSIL. DynamicMethod has an associated type 
//        // DynamicILInfo that can be used in conjunction with  
//        // unmanaged code generators. 
//        // 
//        // The MSIL loads the first argument, which is an instance of 
//        // the Example class, and uses it to load the value of a  
//        // private instance field of type int. The second argument is 
//        // loaded, and the two numbers are multiplied. If the result 
//        // is larger than int, the value is truncated and the most  
//        // significant bits are discarded. The method returns, with 
//        // the return value on the stack. 
//        //
//        ILGenerator ilMH = multiplyHidden.GetILGenerator();
//        ilMH.Emit(OpCodes.Ldarg_0);
//        FieldInfo testInfo = typeof(Example).GetField("test",
//            BindingFlags.NonPublic | BindingFlags.Instance);
//        ilMH.Emit(OpCodes.Ldfld, testInfo);
//        ilMH.Emit(OpCodes.Ldarg_1);
//        ilMH.Emit(OpCodes.Mul);
//        ilMH.Emit(OpCodes.Ret);
//        // Create a delegate that represents the dynamic method.  
//        // Creating the delegate completes the method, and any further  
//        // attempts to change the method � for example, by adding more 
//        // MSIL � are ignored.  
//        //  
//        // The following code binds the method to a new instance 
//        // of the Example class whose private test field is set to 42. 
//        // That is, each time the delegate is invoked the instance of 
//        // Example is passed to the first parameter of the method. 
//        // 
//        // The delegate OneParameter is used, because the first 
//        // parameter of the method receives the instance of Example. 
//        // When the delegate is invoked, only the second parameter is 
//        // required.  
//        //
//        OneParameter<int, int> invoke = (OneParameter<int, int>)
//            multiplyHidden.CreateDelegate(
//                typeof(OneParameter<int, int>), 
//                new Example(42)
//            );
//        Console.WriteLine("3 * test = {0}", invoke(3));
//    }
//}
//* This code example produces the following output:
//123456789 squared = 15241578750190521
//3 * test = 126
// */
#endregion
#region// Specifying Fully Qualified Type Names
// .NET Framework 4.5  
// Other Versions  
// This topic has not yet been rated - Rate this topic  
//You must specify type names to have valid input to various reflection operations. A fully qualified type name consists of an assembly name specification, a namespace specification, and a type name. Type name specifications are used by methods such as TypeGetType, ModuleGetType, ModuleBuilderGetType, and AssemblyGetType.
#endregion
#region// Backus-Naur Form Grammar for Type Names
//--------------------------------------------------------------------------------
//The Backus-Naur form (BNF) defines the syntax of formal languages. The following table lists BNF lexical rules that describe how to recognize a valid input. 
//Terminals (those elements that are not further reducible) are shown in all uppercase letters. Nonterminals (those elements that are further reducible) are shown 
//in mixed-case or singly quoted strings, but the single quote (') is not a part of the syntax itself. The pipe character (|) denotes rules that have subrules.
//
//BNF grammar of fully qualified type names
//TypeSpec                          :=   ReferenceTypeSpec
//                                        |     SimpleTypeSpec
//ReferenceTypeSpec            :=   SimpleTypeSpec '&'
//SimpleTypeSpec                :=   PointerTypeSpec
//                                        |     ArrayTypeSpec
//                                        |     TypeName
//PointerTypeSpec                :=   SimpleTypeSpec '*'
//ArrayTypeSpec                  :=   SimpleTypeSpec '[ReflectionDimension]'
//                                        |     SimpleTypeSpec '[ReflectionEmitDimension]'
//ReflectionDimension           :=   '*'
//                                        |     ReflectionDimension ',' ReflectionDimension
//                                        |     NOTOKEN
//ReflectionEmitDimension    :=   '*'
//                                        |     Number '..' Number
//                                        |     Number '…'
//                                        |     ReflectionDimension ',' ReflectionDimension
//                                        |     NOTOKEN
//Number                            :=   [0-9]+
//TypeName                         :=   NamespaceTypeName
//                                        |     NamespaceTypeName ',' AssemblyNameSpec
//NamespaceTypeName        :=   NestedTypeName
//                                        |     NamespaceSpec '.' NestedTypeName
//NestedTypeName               :=   IDENTIFIER
//                                        |     NestedTypeName '+' IDENTIFIER
//NamespaceSpec                 :=   IDENTIFIER
//                                        |     NamespaceSpec '.' IDENTIFIER
//AssemblyNameSpec           :=   IDENTIFIER
//                                        |     IDENTIFIER ',' AssemblyProperties
//AssemblyProperties            :=   AssemblyProperty
//                                        |     AssemblyProperties ',' AssemblyProperty
//AssemblyProperty              :=   AssemblyPropertyName '=' AssemblyPropertyValue
//Specifying Special Characters 
//--------------------------------------------------------------------------------
//In a type name, IDENTIFIER is any valid name determined by the rules of a language.
//Use the backslash (\) as an escape character to separate the following tokens when used as part of IDENTIFIER.
//Token
//Meaning
//\,
//Assembly separator.
//\+
//Nested type separator.
//\&
//Reference type.
//\*
//Pointer type.
//\[
//Array dimension delimiter.
//\]
//Array dimension delimiter.
//\.
//Use the backslash before a period only if the period is used in an array specification. Periods in NamespaceSpec do not take the backslash.
//\\
//Backslash when needed as a string literal.
//Note that in all TypeSpec components except AssemblyNameSpec, spaces are relevant. In the AssemblyNameSpec, spaces before the ',' separator are relevant, but spaces after the ',' separator are ignored.
//Reflection classes, such as TypeFullName, return the mangled name so that the returned name can be used in a call to GetType, as in MyType.GetType(myType.FullName).
//For example, the fully qualified name for a type might be Ozzy.OutBack.Kangaroo+Wallaby,MyAssembly.
//If the namespace were Ozzy.Out+Back, then the plus sign must be preceded by a backslash. Otherwise, the parser would interpret it as a nesting separator. Reflection emits this string as Ozzy.Out\+Back.Kangaroo+Wallaby,MyAssembly.
#endregion
#region// Specifying Assembly Names 
//--------------------------------------------------------------------------------
//The minimum information required in an assembly name specification is the textual name (IDENTIFIER) of the assembly. You can follow the IDENTIFIER by a 
//comma-separated list of property/value pairs as described in the following table. IDENTIFIER naming should follow the rules for file naming. The IDENTIFIER 
//is case-insensitive.
//
//Property name
//Description
//Allowable values
//Version 
//Assembly version number
//Major.Minor.Build.Revision, where Major, Minor, Build, and Revision are integers between 0 and 65535 inclusive.
//PublicKey 
//Full public key
//String value of full public key in hexadecimal format. Specify a null reference (Nothing in Visual Basic) to explicitly indicate a private assembly.
//PublicKeyToken 
//Public key token (8-byte hash of the full public key)
//String value of public key token in hexadecimal format. Specify a null reference (Nothing in Visual Basic) to explicitly indicate a private assembly.
//Culture 
//Assembly culture
//Culture of the assembly in RFC-1766 format, or "neutral" for language-independent (nonsatellite) assemblies.
//Custom 
//Custom binary large object (BLOB). This is currently used only in assemblies generated by the Native Image Generator (Ngen).
//Custom string used by the Native Image Generator tool to notify the assembly cache that the assembly being installed is a native image, and is therefore to be 
//installed in the native image cache. Also called a zap string.
//
//The following example shows an AssemblyName for a simply named assembly with default culture.
//C#
//Copy
//com.microsoft.crypto, Culture="" 
//The following example shows a fully specified reference for a strongly named assembly with culture "en".
//C#
//Copy
//com.microsoft.crypto, Culture=en, PublicKeyToken=a5d015c7d5a0b012,
//    Version=1.0.0.0 
//The following examples each show a partially specified AssemblyName, which can be satisfied by either a strong or a simply named assembly.
//C#
//Copy
//com.microsoft.crypto
//com.microsoft.crypto, Culture=""
//com.microsoft.crypto, Culture=en 
//The following examples each show a partially specified AssemblyName, which must be satisfied by a simply named assembly.
//C#
//Copy
//com.microsoft.crypto, Culture="", PublicKeyToken=null 
//com.microsoft.crypto, Culture=en, PublicKeyToken=null
//The following examples each show a partially specified AssemblyName, which must be satisfied by a strongly named assembly.
//C#
//Copy
//com.microsoft.crypto, Culture="", PublicKeyToken=a5d015c7d5a0b012
//com.microsoft.crypto, Culture=en, PublicKeyToken=a5d015c7d5a0b012,
//    Version=1.0.0.0
#endregion
#region// Specifying Pointers 
//--------------------------------------------------------------------------------
//SimpleTypeSpec* represents an unmanaged pointer. For example, to get a pointer to type MyType, use Type.GetType("MyType*"). To get a pointer to a pointer to type MyType, use Type.GetType("MyType**").
//Specifying References 
 //SimpleTypeSpec & represents a managed pointer or reference. For example, to get a reference to type MyType, use Type.GetType("MyType &"). Note that unlike pointers, references are limited to one level.
#endregion
//--------------------------------------------------------------------------------//
//              |
              //|\,
             // | \+
            //  |  \&
           //   |   \*
          //    |    \[
         //     |     \]
        //      |      \.
       //       |       \\
#region// Specifying Arrays
//--------------------------------------------------------------------------------
//In the BNF Grammar, ReflectionEmitDimension only applies to incomplete type definitions retrieved using ModuleBuilderGetType. Incomplete type definitions are TypeBuilder objects constructed using Reflection.Emit but on which TypeBuilderCreateType has not been called. ReflectionDimension can be used to retrieve any type definition that has been completed, that is, a type that has been loaded.
//Arrays are accessed in reflection by specifying the rank of the array: 
//•
//Type.GetType("MyArray[]") gets a single-dimension array with 0 lower bound.
//•
//Type.GetType("MyArray[*]") gets a single-dimension array with unknown lower bound.
//•
//Type.GetType("MyArray[][]") gets a two-dimensional array's array.
//•
//Type.GetType("MyArray[*,*]") gets a rectangular two-dimensional array with unknown lower bounds.
//and Type.GetType("MyArray[,]") gets a rectangular two-dimensional array with unknown lower bounds.
//Note that from a runtime point of view, MyArray[] is not the same as what is MyArray[*], but for multidimensional arrays, the two notations are equivalent. That is, Type.GetType("MyArray [,]") == Type.GetType("MyArray[*,*]") evaluates to true.
//For ModuleBuilder.GetType, MyArray[0..5] indicates a single-dimension array with size 6, lower bound 0. MyArray[4…] indicates a single-dimension array of unknown size and lower bound 4.
#endregion
//  Copyright Data Pro Financial Services, Inc. (C) 2004-2013
// Copyright GreenSkyware, Inc. (C) 2013
// Joey Brock - Author - Tampa, FL 33626
// jybrock[AT]dpro[DOM]
#define UNSAFEINJECT
#define UNSAFESTATIC
#define IDREFLECTION
#define ENUMRESOURCE
#define BASECLASS
#define RESOURCES
#define SAFE
#undef  RESOURCES
#undef  BASECLASS
#undef  ENUMRESOURCE
#undef  IDREFLECTION
#undef  UNSAFESTATIC
#undef  UNSAFEINJECT
// jybrock[AT]dpro[DOM]
// Joey Brock - Author - Tampa, FL 33626
// Copyright GreenSkyware, Inc. (C) 2013
//  Copyright Data Pro Financial Services, Inc. (C) 2004-2013
#region USINGS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DpfsLinkTestApplication;
using System.Diagnostics;
using DpfsLinkTestApplication.__Console__;
using DpfsLinkTestApplication.__Monster__;
using DpfsLinkTestApplication.__SOAP__;
using DpfsLinkTestApplication.DEBUG;
using DpfsLinkTestApplication.com.dpro.www;
#if IDREFLECTION
using System.Reflection;
#endif
#endregion
#if IDREFLECTION
namespace System.ComponentModel {
    // Summary:
    //     Specifies the visibility a property has to the design-time serializer.
    [ComVisible(true)]
    public enum DesignerSerializationVisibility {
        // Summary:
        //     The code generator does not produce code for the object.
        Hidden = 0,
        //
        // Summary:.
        //     The code generator produces code for the object.
        Visible = 1,
        //
        // Summary:
        //     The code generator produces code for the contents of the object, rather than
        //     for the object itself.
        Content = 2,
    }
}
#endif
#if UNSAFEINJECT
#endif
#if IDREFLECTION
using System;
using System.Reflection,
namespace System.ComponentModel {
    // Summary:
    //     Specifies the visibility a property has to the design-time serializer.
    [ComVisible(true)]
    public enum DesignerSerializationVisibility {
        // Summary:
        //     The code generator does not produce code for the object.
        Hidden = 0,
        //
        // Summary:.
        //     The code generator produces code for the object.
        Visible = 1,
        //
        // Summary:
        //     The code generator produces code for the contents of the object, rather than
        //     for the object itself.
        Content = 2,
    }
}
#endif
namespace DpfsLinkTestApplication.DEBUG {
   public partial class __debug__  {

#if DEBUG
        //#error DEBUG is defined
        //#pragma checksum "DebugForm.cs" "{D57B4C67-C4CC-409B-8BFA-4322DB9C8DA4}" "0xd57b4c67" // New checksum
#endif
#region PragmaChecksumPreCompiler
//--------------------------------------------------------------------------------
//Generates checksums for source files to aid with debugging ASP.NET pages.
//            "filename"  
//The name of the file that requires monitoring for changes or updates.
//"{guid}"  
//The Globally Unique Identifier (GUID) for the file.
//"checksum_bytes"  
//The string of hexadecimal digits representing the bytes of the checksum. Must be an even number of hexadecimal digits. An odd number of digits results in a compile-time warning, and the directive are ignored.
//The Visual Studio debugger uses a checksum to make sure that it always finds the right source. The compiler computes the checksum for a source file, and then emits the output to the program database (PDB) file. The debugger then uses the PDB to compare against the checksum that it computes for the source file.
//This solution does not work for ASP.NET projects, because the computed checksum is for the generated source file, rather than the .aspx file. To address this problem, #pragma checksum provides checksum support for ASP.NET pages.
//When you create an ASP.NET project in Visual C#, the generated source file contains a checksum for the .aspx file, from which the source is generated. The compiler then writes this information into the PDB file.
//If the compiler encounters no #p ragma checksum directive in the file, it computes the checksum and writes the value to the PDB file.
       // #/ pra gma checksum "file.cs" "{3673e4ca-6098-4ec1-890f-8fceb2a794a2}" "{012345678AB}" // New checksum    }
        //#.//pr agma checksum "filename" "{guid}" "checksum bytes"
#endregion
#region IfAndElifPreCompiler
//--------------------------------------------------------------------------------
//You can use the operators == (equality), != (inequality) only to test for true or false . True means the symbol is defined. The statement #if DEBUG has the same meaning as #if (DEBUG == true). You can use the operators && (and), || (or), and ! (not) to evaluate whether multiple symbols have been defined. You can also group symbols and operators with parentheses.
//            #if , along with the #else, #elif, #endif, #define, and #undef directives, lets you include or exclude code based on the existence of one or more symbols. This can be useful when compiling code for a debug build or when compiling for a specific configuration.
//A conditional directive beginning with a #if directive must explicitly be terminated with a #endif directive.
//#define  lets you define a symbol, such that, by using the symbol as the expression passed to the #if directive, the expression will evaluate to true.
//You can also define a symbol with the /define compiler option. You can undefine a symbol with #undef.
//A symbol that you define with /define or with #define does not conflict with a variable of the same name. That is, a variable name should not be passed to a preprocessor directive and a symbol can only be evaluated by a preprocessor directive.
//The scope of a symbol created with #define is the file in which it was defined.
        // ...
#endregion
#region LinePreCompiler
//--------------------------------------------------------------------------------
//The #line directive might be used in an automated, intermediate step in the build process. For example, if lines were removed from the original source code file, but you still wanted the compiler to generate output based on the original line numbering in the file, you could remove lines and then simulate the original line numbering with #line.
//The #line hidden directive hides the successive lines from the debugger, such that when the developer steps through the code, any lines between a #line hidden and the next #line directive (assuming that it is not another #line hidden directive) will be stepped over. This option can also be used to allow ASP.NET to differentiate between user-defined and machine-generated code. Although ASP.NET is the primary consumer of this feature, it is likely that more source generators will make use of it.
//A #line hidden directive does not affect file names or line numbers in error reporting. That is, if an error is encountered in a hidden block, the compiler will report the current file name and line number of the error.
//The #line filename directive specifies the file name you want to appear in the compiler output. By default, the actual name of the source code file is used. The file name must be in double quotation marks ("") and must be preceded by a line number.
////A source code file can have any number of #line directives.
//#line 200 "Special"
//            int i;    // CS0168 on line 200
//            int j;    // CS0168 on line 201
//#line default
//            char c;   // CS0168 on line 9
//            float f;  // CS0168 on line 10
//#line hidden // numbering not affected
//            string s;
//            double d; // CS0168 on line 13
//            //    static void Main()
//            //    {
//            //#line 200 "Special"
//            //        int i;    // CS0168 on line 200
//            //        int j;    // CS0168 on line 201
//            //#line default
//            //        char c;   // CS0168 on line 9
//            //        float f;  // CS0168 on line 10
//            //#line hidden // numbering not affected
//            //        string s; 
//            //        double d; // CS0168 on line 13
//            //    }
//            //}
//            //
//            // Another Example : 
//            //--------------------------------------------------------------------------------
//            //The following example shows how the debugger ignores the hidden lines in the code. When you run the example, it will display three lines of text. However, when you set a break point, as shown in the example, and hit F10 to step through the code, you will notice that the debugger ignores the hidden line. Notice also that even if you set a break point at the hidden line, the debugger will still ignore it.
//            Console.WriteLine("Normal line #1."); // Set break point here.
//#line hidden
//            Console.WriteLine("Hidden line.");
//#line default
//            Console.WriteLine("Normal line #2.");
//            do {
//                string LineDefinePreCompileInstructions = "#line [ number ['file_name'] | hidden | default ]' = 'numberThe number you want to specify for the following line in a source code file.' }'file_name' (optional)The file name you want to appear in the compiler output. By default, the actual name of the source code file is used. The file name must be in double quotation marks ('').hiddenHides the successive lines from the debugger until another #line directive is encountered.defaultResets the line numbering in a file.";
//            } while (__Console__.__console__.intLoopsWaitingforUI < 40);
//        }
//    static void Main()
//    {
//#line 200 "Special"
//        int i;    // CS0168 on line 200
//        int j;    // CS0168 on line 201
//#line default
//        char c;   // CS0168 on line 9
//        float f;  // CS0168 on line 10
//#line hidden // numbering not affected
//        string s; 
//        double d; // CS0168 on line 13
//    }
//}
#endregion
        //// Most CORRECT way
        //// Only the Dictionary "Interface" is available.
        //// One static object.
        //// 
        //// Lightweight Version
        #region __DEBUG__ Interface Capable Public Properties
        public static IDictionary<string, string> DebugDataSources { get; set; }
        #endregion
        #region __DEBUG__ Protected Private Fields
        private  Dictionary<string, string> debugDataSources = new Dictionary<string, string>(DebugDataSources);
        //private static __DEBUG__ __DEBUG__ = new __DEBUG__.__DEBUG__();
        #endregion
        #region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        private Dictionary<string, string> _debugDataSources() {
#if UNSAFESTATIC
            DPList = new Dictionary<string,string>();
            DPList.Add("CountAccess","string");
            DPList.Add("GetItem", "string");
            DPList.Add("GetProject", "string");
            DPList.Add("ItemToProject", "string");
            DPList.Add("ItemToProjectAccess", "string");
            DPList.Add("ItemTransfer", "string");
            DPList.Add("ItemTransferAccess", "string");
            DPList.Add("ReceivingAccess", "string");
            DPList.Add("ReceivingPostReceipt", "string");
            DPList.Add("ReceivingValidateItem ", "string");
            DPList.Add("ReceivingValidatePO", "string");
            DPList.Add("ShippingAccess", "string");
            DPList.Add("ShippingPostShipment ", "string");
            DPList.Add("ShippingValidateItem", "string");
            DPList.Add("ShippingValidateSO ", "string");
            DPList.Add("UpdateCount", "string");
            DictListDPServices = DPList;
#endif
            return DebugDataSources;
        }
        #endregion
        #region INTERFACE CAPABLE PUBLIC PROPERTIES
        #endregion
        #region PROTECTED PRIVATE FIELDS
        #endregion
        #region COLLECTIONS
        #endregion
        //
        // 1 Get Resources Available 
        // Create instances of anything and everything in a base class
        // spawn off as many protected partial Classes as possible(for every object enumerable)
        // gather intel on surroundings in a structure 100% my own
#if SAFE
        //// Most CORRECT way
        //// Only the Dictionary "Interface" is available.
        //// One static object.
        //// 
        //// Lightweight Version
        //#region __DEBUG__ Interface Capable Public Properties
        //public static IDictionary<string, string> DebugDataSources { get; set; }
        //#endregion
        //#region __DEBUG__ Protected Private Fields
        //private static IDictionary<string, string> DebugDataSources = new Dictionary<string, string>(_debugDataSources);
        //#endregion
        //#region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        //private IDictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
        //#endregion
        //// Most DANGEROUS way
        //// Accesses the same Dictionary<TKey,TValue> Collection :
        //// #1 Public Static Interface to a Dictionary (Complete Instance)
        //// #2 Public Static Dictionary Full Read/Write Access (Like Partial Class - Could Impersonate)
        //// 
        //// 
        //// Lightweight Version
#if UNSAFEINJECT
        //#region __DEBUG__ Interface Capable Public Properties   //#2
        //public static Dictionary<string, string> DebugDataSources { get; set; }
        //#endregion
        //#region __DEBUG__ Protected Private Fields              //#1
        //public static IDictionary<string, string> debugDataSources = new Dictionary<string, string>(DebugDataSources);
        //#endregion
        //#region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        //private static IDictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
       // #endregion
#endif
        ////// 
        ////// Lightweight Version
        #region __DEBUG__ Interface Capable Public Properties
        //public static Dictionary<string, string> debugDataSources { get; set; }
        #endregion
        #region __DEBUG__ Protected Private Fields
        //public static IDictionary<string, string> DebugDataSources = new Dictionary<string, string>(DebugDataSources);
        #endregion
        #region __DEBUG__  Dictionary<TKey,TValue> Collections
        //private IDictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
        #endregion
        //#region __DEBUG__ Interface Capable Public Properties
        //public static IDictionary<string, string> DebugDataSources = new Dictionary<string, string>(debugDataSources);
        //#endregion
        //#region __DEBUG__ Protected Private Fields
        //protected static IDictionary<string, string> debugDataSources { get; set; }
        //#endregion
        //#region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        //private static IDictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
        //#endregion
        //#region __DEBUG__ Interface Capable Public Properties
        //public static Dictionary<string, string> DebugDataSources { get; set; }
        //#endregion
        //#region __DEBUG__ Protected Private Fields 
        //protected static IDictionary<string, string> debugDataSources = new IDictionary<string, string>(DebugDataSources);
        //#endregion
        //#region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        //private static Dictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
        //#endregion
        //#region __DEBUG__ Public Properties
        //public static Dictionary<string, string> DebugDataSources { get; set; }
        //private static IDictionary<string, string> debugDataSources { get; set; }
        //#endregion
        //#region __DEBUG__ Private Fields
        //public static Dictionary<string, string> DebugDataSources = new Dictionary<string, string>(DebugDataSources);
        //private static IDictionary<string, string> debugDataSources = new Dictionary<string, string>(DebugDataSources);
        //#endregion
        //#region __DEBUG__ Generates Dictionary<TKey,TValue> Collections
        //private static IDictionary<string, string> _debugDataSources() {
        //    return debugDataSources;
        //}
        //#endregion


/// <summary>
        /// Drop Down ComboBox for Choosing a Dataset to Work from.
        /// Drag and Drop from Collections generated by Console, etc.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void comboBoxDebugList_SelectedIndexChanged(object sender, EventArgs e) {
        }
#if UNSAFEINJECT
        [Conditional("DEBUG")]
        public static void Message(string traceMessage) {
            Console.WriteLine("[TRACE] - " + traceMessage);
        } 
#endif

    }
}
#endif