#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct InvokerActionInvoker1
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1)
	{
		void* params[1] = { &p1 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1>
struct InvokerActionInvoker1<T1*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2 p2)
	{
		void* params[2] = { p1, &p2 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};

// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB;
// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Collections.Generic.Dictionary`2<System.Int32,System.Globalization.CultureInfo>
struct Dictionary_2_t9FA6D82CAFC18769F7515BB51D1C56DAE09381C3;
// System.Collections.Generic.Dictionary`2<System.Int32,System.Threading.Tasks.Task>
struct Dictionary_2_t403063CE4960B4F46C688912237C6A27E550FF55;
// System.Collections.Generic.Dictionary`2<System.String,System.Globalization.CultureInfo>
struct Dictionary_2_tE1603CE612C16451D1E56FF4D4859D4FE4087C28;
// System.Func`1<System.Object>
struct Func_1_tD5C081AE11746B200C711DD48DBEB00E3A9276D4;
// System.Func`1<System.String>
struct Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C;
// System.Func`1<System.Threading.Tasks.Task/ContingentProperties>
struct Func_1_tD59A12717D79BFB403BF973694B1BE5B85474BD1;
// System.Predicate`1<System.Object>
struct Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12;
// System.Predicate`1<System.Threading.Tasks.Task>
struct Predicate_1_t7F48518B008C1472339EEEBABA3DE203FE1F26ED;
// System.Threading.Tasks.TaskCompletionSource`1<System.Int32Enum>
struct TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A;
// System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission>
struct TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531;
// System.Threading.Tasks.TaskFactory`1<System.Int32Enum>
struct TaskFactory_1_t96AF1AA119B568BA8916E7FD621B61B350B9BB49;
// System.Threading.Tasks.TaskFactory`1<System.String>
struct TaskFactory_1_t7ABCD7F9503486A075E0B072E6EB95956CFE3106;
// System.Threading.Tasks.TaskFactory`1<UnityEngine.Texture2D>
struct TaskFactory_1_tA02CD66EEE1C447BE2C3BDAF4821F919418AE5FF;
// System.Threading.Tasks.TaskFactory`1<NativeCamera/Permission>
struct TaskFactory_1_t7E63F6634D69B0FEA76FDA2DA921B250C00B353F;
// System.Threading.Tasks.Task`1<System.Int32Enum>
struct Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB;
// System.Threading.Tasks.Task`1<System.Object>
struct Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2;
// System.Threading.Tasks.Task`1<System.String>
struct Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0;
// System.Threading.Tasks.Task`1<UnityEngine.Texture2D>
struct Task_1_t95921EB64E237ACD28589D64B693C652268F225E;
// System.Threading.Tasks.Task`1<NativeCamera/Permission>
struct Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
// UnityEngine.AndroidJavaClass
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03;
// UnityEngine.AndroidJavaObject
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0;
// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D;
// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C;
// System.Globalization.Calendar
struct Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B;
// UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804;
// System.Globalization.CompareInfo
struct CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57;
// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
// System.Threading.ContextCallback
struct ContextCallback_tE8AFBDBFCC040FDA8DA8C1EEFE9BD66B16BDA007;
// System.Globalization.CultureData
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D;
// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0;
// System.Globalization.DateTimeFormatInfo
struct DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.IO.DirectoryInfo
struct DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2;
// UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB;
// System.Exception
struct Exception_t;
// System.IO.FileNotFoundException
struct FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A;
// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
// UnityEngine.GlobalJavaObjectRef
struct GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// System.Runtime.CompilerServices.IAsyncStateMachine
struct IAsyncStateMachine_t0680C7F905C553076B552D5A1A6E39E2F0F36AA2;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.IFormatProvider
struct IFormatProvider_tC202922D43BFF3525109ABF3FB79625F5646AB52;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// NativeCameraNamespace.NCCallbackHelper
struct NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA;
// NativeCameraNamespace.NCCameraCallbackAndroid
struct NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378;
// NativeCameraNamespace.NCPermissionCallbackAndroid
struct NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91;
// NativeCameraNamespace.NCPermissionCallbackAsyncAndroid
struct NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B;
// System.Globalization.NumberFormatInfo
struct NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.Threading.SendOrPostCallback
struct SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E;
// System.Threading.Tasks.StackGuard
struct StackGuard_tACE063A1B7374BDF4AD472DE4585D05AD8745352;
// System.String
struct String_t;
// System.Threading.Tasks.TaskFactory
struct TaskFactory_tF781BD37BE23917412AD83424D1497C7C1509DF0;
// System.Threading.Tasks.TaskScheduler
struct TaskScheduler_t3F0550EBEF7C41F74EC8C08FF4BED0D8CE66006E;
// System.Globalization.TextInfo
struct TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4;
// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
// UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F;
// UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C;
// UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6;
// System.Uri
struct Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// System.Threading.WaitCallback
struct WaitCallback_tFB2C7FD58D024BBC2B0333DC7A4CB63B8DEBD5D3;
// NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0
struct U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850;
// NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0
struct U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32;
// NativeCamera/<>c__DisplayClass20_0
struct U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E;
// NativeCamera/<>c__DisplayClass28_0
struct U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB;
// NativeCamera/<>c__DisplayClass30_0
struct U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A;
// NativeCamera/CameraCallback
struct CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771;
// NativeCamera/PermissionCallback
struct PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001;
// System.Threading.Tasks.Task/ContingentProperties
struct ContingentProperties_t3FA59480914505CEA917B1002EC675F29D0CB540;

IL2CPP_EXTERN_C RuntimeClass* Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Permission_t1EEFB992D4BAC04AB077BD49556F33B8B6E0E9F8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0443845674FDE6986E4ECC72A8C096004DF51FC6;
IL2CPP_EXTERN_C String_t* _stringLiteral0A051229B6EA622EA0A0C01F91FB09E1623476AA;
IL2CPP_EXTERN_C String_t* _stringLiteral0CA4721FC9D82D780671DE2AB61257837402697D;
IL2CPP_EXTERN_C String_t* _stringLiteral1256BD059A8D156C0578EF505C83E5862F0EFCD2;
IL2CPP_EXTERN_C String_t* _stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8;
IL2CPP_EXTERN_C String_t* _stringLiteral18B82B6B7DC4FE1988BA61A3784D1768F6C925DF;
IL2CPP_EXTERN_C String_t* _stringLiteral218F5A08519088A96BE3C1074984C53EA49F1CCA;
IL2CPP_EXTERN_C String_t* _stringLiteral22965B1EE1F6EB152925F32087BD48959BA8C1BC;
IL2CPP_EXTERN_C String_t* _stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A;
IL2CPP_EXTERN_C String_t* _stringLiteral3E96C9BB1B953A85290371E8CE7BB3F3ABB307CC;
IL2CPP_EXTERN_C String_t* _stringLiteral491B4D9271839F0BD63211437BF7CEE5B2C6ADE9;
IL2CPP_EXTERN_C String_t* _stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42;
IL2CPP_EXTERN_C String_t* _stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078;
IL2CPP_EXTERN_C String_t* _stringLiteral5D67C2D23D0E67BA40CD70B037A9F218807BB46F;
IL2CPP_EXTERN_C String_t* _stringLiteral60765CA56083EB814A8E566B08E35D91D1000DEC;
IL2CPP_EXTERN_C String_t* _stringLiteral660F6D5965E09393894520A3BBDDAE9F5DEF81D2;
IL2CPP_EXTERN_C String_t* _stringLiteral75E05143EB132AAA8A22B48813DB8E6047380821;
IL2CPP_EXTERN_C String_t* _stringLiteral76F1B85647641622FD867CE16AF6C584C5081BD4;
IL2CPP_EXTERN_C String_t* _stringLiteral811A5CF8DF2E4D4CA546FECCBB9A503DC8D89283;
IL2CPP_EXTERN_C String_t* _stringLiteral8A4D2503591255173AC6769BB8A784B9CC5B5BC6;
IL2CPP_EXTERN_C String_t* _stringLiteral985B72B30ECE05DD4EF5FE142CEE0FB8BF53A98C;
IL2CPP_EXTERN_C String_t* _stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26;
IL2CPP_EXTERN_C String_t* _stringLiteralA3A21FB44DD18299A19A0B86BA27CEB4EDA6A941;
IL2CPP_EXTERN_C String_t* _stringLiteralAFF4AA19F30B5DC5A240F413D92917103536F1AD;
IL2CPP_EXTERN_C String_t* _stringLiteralBB89381AF6FA67A07F15BA3C2582693F59E071ED;
IL2CPP_EXTERN_C String_t* _stringLiteralCB4507437E3E619ECBAD84410155675EBEB3DB3F;
IL2CPP_EXTERN_C String_t* _stringLiteralDD146CE30524569A8784D1FFE34EA505C910727D;
IL2CPP_EXTERN_C String_t* _stringLiteralE3287997374A5B6321B447152239EBE224279EB6;
IL2CPP_EXTERN_C String_t* _stringLiteralF0750A05DD548EB8E0F9CD56A424497F03563B9D;
IL2CPP_EXTERN_C String_t* _stringLiteralF811A8F3778A439E75478C3728BE25A7853EAF83;
IL2CPP_EXTERN_C String_t* _stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9;
IL2CPP_EXTERN_C String_t* _stringLiteralFFB00AB07E4FD5417BED928D76EA5DBF492DFDAE;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m4B4C82CEEADEF9B96CFD0819E9DA9DC931389FE4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mA065D9BD25C55B7B2630CA549183C4615FF27DFC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m850BD17D7C2BD7A37C5B5261B69E44A451533191_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m56EC0B7A46A5E522CCEAB37456060C36E26CE731_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m59F56DB7F0E99D8EA793C64E8735398C0539DBE6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mC08662C58644EB7E11271EF5EDFB23D1678CB978_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* NativeCamera_GetImageProperties_m906832D1F45ECF5821CFA3236192AE053761C050_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* NativeCamera_GetVideoProperties_m2E7F06E4A9C55A06A5752F8337951217018250EB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskAwaiter_1_GetResult_mE4B8867B0D8DAA1317AD64FE09FBD26E825A654C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskAwaiter_1_get_IsCompleted_m77FF413EE49A5859C0BC111104448D64F3C01911_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskCompletionSource_1_SetResult_mE3BD8B51FE6830985D8550B8E7F71C413B087604_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskCompletionSource_1__ctor_m59FEBA96E2C649BB6B9E4A681806C7ED628834FB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TaskCompletionSource_1_get_Task_mDCD7F80204B3108705062BD8783CD541E08A4157_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Task_1_GetAwaiter_m88AFED53B032F7EDDB6F9746699970B9FFFC992C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass20_0_U3CRequestPermissionAsyncU3Eb__0_m5DF168C3CFBC1336DC14F7F3C6B889DF554A7ED1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass28_0_U3CLoadImageAtPathAsyncU3Eb__0_m50534284F00FF8C07BABF2F72B8E9EA780CBF16E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass30_0_U3CGetVideoThumbnailAsyncU3Eb__0_m5DAF2A26314559841D948DC75B130034596DF868_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass3_0_U3COnMediaReceivedU3Eb__0_m2F42366AEBB844734D6B392AF6A392BF2BF19458_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass3_0_U3COnPermissionResultU3Eb__0_m18426273EFEC1619EDB4B879CA210DD0957D2796_RuntimeMethod_var;
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com;
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_com;
struct CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_pinvoke;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com;
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke;
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tF0E9D9D49305425E48612F9F1BC145ECE6B9E574 
{
};

// System.Threading.Tasks.TaskCompletionSource`1<System.Int32Enum>
struct TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A  : public RuntimeObject
{
	// System.Threading.Tasks.Task`1<TResult> System.Threading.Tasks.TaskCompletionSource`1::_task
	Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB* ____task_0;
};

// System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission>
struct TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531  : public RuntimeObject
{
	// System.Threading.Tasks.Task`1<TResult> System.Threading.Tasks.TaskCompletionSource`1::_task
	Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED* ____task_0;
};

// UnityEngine.AndroidJavaObject
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0  : public RuntimeObject
{
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaObject::m_jobject
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jobject_1;
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaObject::m_jclass
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jclass_2;
};

struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_StaticFields
{
	// System.Boolean UnityEngine.AndroidJavaObject::enableDebugPrints
	bool ___enableDebugPrints_0;
};
struct Il2CppArrayBounds;

// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0  : public RuntimeObject
{
	// System.Boolean System.Globalization.CultureInfo::m_isReadOnly
	bool ___m_isReadOnly_3;
	// System.Int32 System.Globalization.CultureInfo::cultureID
	int32_t ___cultureID_4;
	// System.Int32 System.Globalization.CultureInfo::parent_lcid
	int32_t ___parent_lcid_5;
	// System.Int32 System.Globalization.CultureInfo::datetime_index
	int32_t ___datetime_index_6;
	// System.Int32 System.Globalization.CultureInfo::number_index
	int32_t ___number_index_7;
	// System.Int32 System.Globalization.CultureInfo::default_calendar_type
	int32_t ___default_calendar_type_8;
	// System.Boolean System.Globalization.CultureInfo::m_useUserOverride
	bool ___m_useUserOverride_9;
	// System.Globalization.NumberFormatInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::numInfo
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	// System.Globalization.DateTimeFormatInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::dateTimeInfo
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	// System.Globalization.TextInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::textInfo
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	// System.String System.Globalization.CultureInfo::m_name
	String_t* ___m_name_13;
	// System.String System.Globalization.CultureInfo::englishname
	String_t* ___englishname_14;
	// System.String System.Globalization.CultureInfo::nativename
	String_t* ___nativename_15;
	// System.String System.Globalization.CultureInfo::iso3lang
	String_t* ___iso3lang_16;
	// System.String System.Globalization.CultureInfo::iso2lang
	String_t* ___iso2lang_17;
	// System.String System.Globalization.CultureInfo::win3lang
	String_t* ___win3lang_18;
	// System.String System.Globalization.CultureInfo::territory
	String_t* ___territory_19;
	// System.String[] System.Globalization.CultureInfo::native_calendar_names
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___native_calendar_names_20;
	// System.Globalization.CompareInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::compareInfo
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	// System.Void* System.Globalization.CultureInfo::textinfo_data
	void* ___textinfo_data_22;
	// System.Int32 System.Globalization.CultureInfo::m_dataItem
	int32_t ___m_dataItem_23;
	// System.Globalization.Calendar System.Globalization.CultureInfo::calendar
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::parent_culture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___parent_culture_25;
	// System.Boolean System.Globalization.CultureInfo::constructed
	bool ___constructed_26;
	// System.Byte[] System.Globalization.CultureInfo::cached_serialized_form
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___cached_serialized_form_27;
	// System.Globalization.CultureData System.Globalization.CultureInfo::m_cultureData
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D* ___m_cultureData_28;
	// System.Boolean System.Globalization.CultureInfo::m_isInherited
	bool ___m_isInherited_29;
};

struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_StaticFields
{
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::invariant_culture_info
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___invariant_culture_info_0;
	// System.Object System.Globalization.CultureInfo::shared_table_lock
	RuntimeObject* ___shared_table_lock_1;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::default_current_culture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___default_current_culture_2;
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::s_DefaultThreadCurrentUICulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_DefaultThreadCurrentUICulture_34;
	// System.Globalization.CultureInfo modreq(System.Runtime.CompilerServices.IsVolatile) System.Globalization.CultureInfo::s_DefaultThreadCurrentCulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_DefaultThreadCurrentCulture_35;
	// System.Collections.Generic.Dictionary`2<System.Int32,System.Globalization.CultureInfo> System.Globalization.CultureInfo::shared_by_number
	Dictionary_2_t9FA6D82CAFC18769F7515BB51D1C56DAE09381C3* ___shared_by_number_36;
	// System.Collections.Generic.Dictionary`2<System.String,System.Globalization.CultureInfo> System.Globalization.CultureInfo::shared_by_name
	Dictionary_2_tE1603CE612C16451D1E56FF4D4859D4FE4087C28* ___shared_by_name_37;
	// System.Globalization.CultureInfo System.Globalization.CultureInfo::s_UserPreferredCultureInfoInAppX
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___s_UserPreferredCultureInfoInAppX_38;
	// System.Boolean System.Globalization.CultureInfo::IsTaiwanSku
	bool ___IsTaiwanSku_39;
};
// Native definition for P/Invoke marshalling of System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke
{
	int32_t ___m_isReadOnly_3;
	int32_t ___cultureID_4;
	int32_t ___parent_lcid_5;
	int32_t ___datetime_index_6;
	int32_t ___number_index_7;
	int32_t ___default_calendar_type_8;
	int32_t ___m_useUserOverride_9;
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	char* ___m_name_13;
	char* ___englishname_14;
	char* ___nativename_15;
	char* ___iso3lang_16;
	char* ___iso2lang_17;
	char* ___win3lang_18;
	char* ___territory_19;
	char** ___native_calendar_names_20;
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	void* ___textinfo_data_22;
	int32_t ___m_dataItem_23;
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke* ___parent_culture_25;
	int32_t ___constructed_26;
	Il2CppSafeArray/*NONE*/* ___cached_serialized_form_27;
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_pinvoke* ___m_cultureData_28;
	int32_t ___m_isInherited_29;
};
// Native definition for COM marshalling of System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com
{
	int32_t ___m_isReadOnly_3;
	int32_t ___cultureID_4;
	int32_t ___parent_lcid_5;
	int32_t ___datetime_index_6;
	int32_t ___number_index_7;
	int32_t ___default_calendar_type_8;
	int32_t ___m_useUserOverride_9;
	NumberFormatInfo_t8E26808B202927FEBF9064FCFEEA4D6E076E6472* ___numInfo_10;
	DateTimeFormatInfo_t0457520F9FA7B5C8EAAEB3AD50413B6AEEB7458A* ___dateTimeInfo_11;
	TextInfo_tD3BAFCFD77418851E7D5CB8D2588F47019E414B4* ___textInfo_12;
	Il2CppChar* ___m_name_13;
	Il2CppChar* ___englishname_14;
	Il2CppChar* ___nativename_15;
	Il2CppChar* ___iso3lang_16;
	Il2CppChar* ___iso2lang_17;
	Il2CppChar* ___win3lang_18;
	Il2CppChar* ___territory_19;
	Il2CppChar** ___native_calendar_names_20;
	CompareInfo_t1B1A6AC3486B570C76ABA52149C9BD4CD82F9E57* ___compareInfo_21;
	void* ___textinfo_data_22;
	int32_t ___m_dataItem_23;
	Calendar_t0A117CC7532A54C17188C2EFEA1F79DB20DF3A3B* ___calendar_24;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com* ___parent_culture_25;
	int32_t ___constructed_26;
	Il2CppSafeArray/*NONE*/* ___cached_serialized_form_27;
	CultureData_tEEFDCF4ECA1BBF6C0C8C94EB3541657245598F9D_marshaled_com* ___m_cultureData_28;
	int32_t ___m_isInherited_29;
};

// System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE  : public RuntimeObject
{
	// System.Object System.MarshalByRefObject::_identity
	RuntimeObject* ____identity_0;
};
// Native definition for P/Invoke marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_pinvoke
{
	Il2CppIUnknown* ____identity_0;
};
// Native definition for COM marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_com
{
	Il2CppIUnknown* ____identity_0;
};

// NativeCamera
struct NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F  : public RuntimeObject
{
};

struct NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields
{
	// UnityEngine.AndroidJavaClass NativeCamera::m_ajc
	AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* ___m_ajc_0;
	// UnityEngine.AndroidJavaObject NativeCamera::m_context
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___m_context_1;
	// System.String NativeCamera::m_temporaryImagePath
	String_t* ___m_temporaryImagePath_2;
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.Threading.Tasks.Task
struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572  : public RuntimeObject
{
	// System.Int32 modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_taskId
	int32_t ___m_taskId_1;
	// System.Delegate System.Threading.Tasks.Task::m_action
	Delegate_t* ___m_action_2;
	// System.Object System.Threading.Tasks.Task::m_stateObject
	RuntimeObject* ___m_stateObject_3;
	// System.Threading.Tasks.TaskScheduler System.Threading.Tasks.Task::m_taskScheduler
	TaskScheduler_t3F0550EBEF7C41F74EC8C08FF4BED0D8CE66006E* ___m_taskScheduler_4;
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::m_parent
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___m_parent_5;
	// System.Int32 modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_stateFlags
	int32_t ___m_stateFlags_6;
	// System.Object modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_continuationObject
	RuntimeObject* ___m_continuationObject_23;
	// System.Threading.Tasks.Task/ContingentProperties modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_contingentProperties
	ContingentProperties_t3FA59480914505CEA917B1002EC675F29D0CB540* ___m_contingentProperties_26;
};

struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_StaticFields
{
	// System.Int32 System.Threading.Tasks.Task::s_taskIdCounter
	int32_t ___s_taskIdCounter_0;
	// System.Object System.Threading.Tasks.Task::s_taskCompletionSentinel
	RuntimeObject* ___s_taskCompletionSentinel_24;
	// System.Boolean System.Threading.Tasks.Task::s_asyncDebuggingEnabled
	bool ___s_asyncDebuggingEnabled_25;
	// System.Action`1<System.Object> System.Threading.Tasks.Task::s_taskCancelCallback
	Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* ___s_taskCancelCallback_27;
	// System.Func`1<System.Threading.Tasks.Task/ContingentProperties> System.Threading.Tasks.Task::s_createContingentProperties
	Func_1_tD59A12717D79BFB403BF973694B1BE5B85474BD1* ___s_createContingentProperties_30;
	// System.Threading.Tasks.TaskFactory System.Threading.Tasks.Task::<Factory>k__BackingField
	TaskFactory_tF781BD37BE23917412AD83424D1497C7C1509DF0* ___U3CFactoryU3Ek__BackingField_31;
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::<CompletedTask>k__BackingField
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___U3CCompletedTaskU3Ek__BackingField_32;
	// System.Predicate`1<System.Threading.Tasks.Task> System.Threading.Tasks.Task::s_IsExceptionObservedByParentPredicate
	Predicate_1_t7F48518B008C1472339EEEBABA3DE203FE1F26ED* ___s_IsExceptionObservedByParentPredicate_33;
	// System.Threading.ContextCallback System.Threading.Tasks.Task::s_ecCallback
	ContextCallback_tE8AFBDBFCC040FDA8DA8C1EEFE9BD66B16BDA007* ___s_ecCallback_34;
	// System.Predicate`1<System.Object> System.Threading.Tasks.Task::s_IsTaskContinuationNullPredicate
	Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* ___s_IsTaskContinuationNullPredicate_35;
	// System.Collections.Generic.Dictionary`2<System.Int32,System.Threading.Tasks.Task> System.Threading.Tasks.Task::s_currentActiveTasks
	Dictionary_2_t403063CE4960B4F46C688912237C6A27E550FF55* ___s_currentActiveTasks_36;
	// System.Object System.Threading.Tasks.Task::s_activeTasksLock
	RuntimeObject* ___s_activeTasksLock_37;
};

struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_ThreadStaticFields
{
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::t_currentTask
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___t_currentTask_28;
	// System.Threading.Tasks.StackGuard System.Threading.Tasks.Task::t_stackGuard
	StackGuard_tACE063A1B7374BDF4AD472DE4585D05AD8745352* ___t_stackGuard_29;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};

// NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0
struct U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850  : public RuntimeObject
{
	// NativeCameraNamespace.NCCameraCallbackAndroid NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0::<>4__this
	NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* ___U3CU3E4__this_0;
	// System.String NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0::path
	String_t* ___path_1;
};

// NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0
struct U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32  : public RuntimeObject
{
	// NativeCameraNamespace.NCPermissionCallbackAsyncAndroid NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0::<>4__this
	NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* ___U3CU3E4__this_0;
	// System.Int32 NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0::result
	int32_t ___result_1;
};

// NativeCamera/<>c__DisplayClass20_0
struct U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E  : public RuntimeObject
{
	// System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission> NativeCamera/<>c__DisplayClass20_0::tcs
	TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* ___tcs_0;
};

// NativeCamera/<>c__DisplayClass28_0
struct U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB  : public RuntimeObject
{
	// System.String NativeCamera/<>c__DisplayClass28_0::imagePath
	String_t* ___imagePath_0;
	// System.String NativeCamera/<>c__DisplayClass28_0::temporaryImagePath
	String_t* ___temporaryImagePath_1;
	// System.Int32 NativeCamera/<>c__DisplayClass28_0::maxSize
	int32_t ___maxSize_2;
};

// NativeCamera/<>c__DisplayClass30_0
struct U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A  : public RuntimeObject
{
	// System.String NativeCamera/<>c__DisplayClass30_0::videoPath
	String_t* ___videoPath_0;
	// System.String NativeCamera/<>c__DisplayClass30_0::temporaryImagePath
	String_t* ___temporaryImagePath_1;
	// System.Int32 NativeCamera/<>c__DisplayClass30_0::maxSize
	int32_t ___maxSize_2;
	// System.Double NativeCamera/<>c__DisplayClass30_0::captureTimeInSeconds
	double ___captureTimeInSeconds_3;
};

// System.Runtime.CompilerServices.TaskAwaiter`1<System.Object>
struct TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA 
{
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.TaskAwaiter`1::m_task
	Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* ___m_task_0;
};

// System.Runtime.CompilerServices.TaskAwaiter`1<System.String>
struct TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 
{
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.TaskAwaiter`1::m_task
	Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* ___m_task_0;
};

// System.Runtime.CompilerServices.TaskAwaiter`1<UnityEngine.Texture2D>
struct TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B 
{
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.TaskAwaiter`1::m_task
	Task_1_t95921EB64E237ACD28589D64B693C652268F225E* ___m_task_0;
};

// System.Threading.Tasks.Task`1<System.Int32Enum>
struct Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	int32_t ___m_result_38;
};

struct Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB_StaticFields
{
	// System.Threading.Tasks.TaskFactory`1<TResult> System.Threading.Tasks.Task`1::s_defaultFactory
	TaskFactory_1_t96AF1AA119B568BA8916E7FD621B61B350B9BB49* ___s_defaultFactory_39;
};

// System.Threading.Tasks.Task`1<System.String>
struct Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	String_t* ___m_result_38;
};

struct Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0_StaticFields
{
	// System.Threading.Tasks.TaskFactory`1<TResult> System.Threading.Tasks.Task`1::s_defaultFactory
	TaskFactory_1_t7ABCD7F9503486A075E0B072E6EB95956CFE3106* ___s_defaultFactory_39;
};

// System.Threading.Tasks.Task`1<UnityEngine.Texture2D>
struct Task_1_t95921EB64E237ACD28589D64B693C652268F225E  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___m_result_38;
};

struct Task_1_t95921EB64E237ACD28589D64B693C652268F225E_StaticFields
{
	// System.Threading.Tasks.TaskFactory`1<TResult> System.Threading.Tasks.Task`1::s_defaultFactory
	TaskFactory_1_tA02CD66EEE1C447BE2C3BDAF4821F919418AE5FF* ___s_defaultFactory_39;
};

// System.Threading.Tasks.Task`1<NativeCamera/Permission>
struct Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	int32_t ___m_result_38;
};

struct Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED_StaticFields
{
	// System.Threading.Tasks.TaskFactory`1<TResult> System.Threading.Tasks.Task`1::s_defaultFactory
	TaskFactory_1_t7E63F6634D69B0FEA76FDA2DA921B250C00B353F* ___s_defaultFactory_39;
};

// UnityEngine.AndroidJavaClass
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03  : public AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0
{
};

// System.Runtime.CompilerServices.AsyncMethodBuilderCore
struct AsyncMethodBuilderCore_tD5ABB3A2536319A3345B32A5481E37E23DD8CEDF 
{
	// System.Runtime.CompilerServices.IAsyncStateMachine System.Runtime.CompilerServices.AsyncMethodBuilderCore::m_stateMachine
	RuntimeObject* ___m_stateMachine_0;
	// System.Action System.Runtime.CompilerServices.AsyncMethodBuilderCore::m_defaultContextAction
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___m_defaultContextAction_1;
};
// Native definition for P/Invoke marshalling of System.Runtime.CompilerServices.AsyncMethodBuilderCore
struct AsyncMethodBuilderCore_tD5ABB3A2536319A3345B32A5481E37E23DD8CEDF_marshaled_pinvoke
{
	RuntimeObject* ___m_stateMachine_0;
	Il2CppMethodPointer ___m_defaultContextAction_1;
};
// Native definition for COM marshalling of System.Runtime.CompilerServices.AsyncMethodBuilderCore
struct AsyncMethodBuilderCore_tD5ABB3A2536319A3345B32A5481E37E23DD8CEDF_marshaled_com
{
	RuntimeObject* ___m_stateMachine_0;
	Il2CppMethodPointer ___m_defaultContextAction_1;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Byte
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	// System.Byte System.Byte::m_value
	uint8_t ___m_value_0;
};

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
};

// System.Double
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	// System.Double System.Double::m_value
	double ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.Int64
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	// System.Int64 System.Int64::m_value
	int64_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Runtime.CompilerServices.YieldAwaitable
struct YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB 
{
	union
	{
		struct
		{
		};
		uint8_t YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB__padding[1];
	};
};

// NativeCamera/ImageProperties
struct ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46 
{
	// System.Int32 NativeCamera/ImageProperties::width
	int32_t ___width_0;
	// System.Int32 NativeCamera/ImageProperties::height
	int32_t ___height_1;
	// System.String NativeCamera/ImageProperties::mimeType
	String_t* ___mimeType_2;
	// NativeCamera/ImageOrientation NativeCamera/ImageProperties::orientation
	int32_t ___orientation_3;
};
// Native definition for P/Invoke marshalling of NativeCamera/ImageProperties
struct ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_pinvoke
{
	int32_t ___width_0;
	int32_t ___height_1;
	char* ___mimeType_2;
	int32_t ___orientation_3;
};
// Native definition for COM marshalling of NativeCamera/ImageProperties
struct ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_com
{
	int32_t ___width_0;
	int32_t ___height_1;
	Il2CppChar* ___mimeType_2;
	int32_t ___orientation_3;
};

// NativeCamera/VideoProperties
struct VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E 
{
	// System.Int32 NativeCamera/VideoProperties::width
	int32_t ___width_0;
	// System.Int32 NativeCamera/VideoProperties::height
	int32_t ___height_1;
	// System.Int64 NativeCamera/VideoProperties::duration
	int64_t ___duration_2;
	// System.Single NativeCamera/VideoProperties::rotation
	float ___rotation_3;
};

// System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter
struct YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A 
{
	union
	{
		struct
		{
		};
		uint8_t YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A__padding[1];
	};
};

struct YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_StaticFields
{
	// System.Threading.WaitCallback System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::s_waitCallbackRunAction
	WaitCallback_tFB2C7FD58D024BBC2B0333DC7A4CB63B8DEBD5D3* ___s_waitCallbackRunAction_0;
	// System.Threading.SendOrPostCallback System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::s_sendOrPostCallbackRunAction
	SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* ___s_sendOrPostCallbackRunAction_1;
};

// Interop/Sys/FileStatus
struct FileStatus_tCB96EDE0D0F945F685B9BBED6DBF0731207458C2 
{
	// Interop/Sys/FileStatusFlags Interop/Sys/FileStatus::Flags
	int32_t ___Flags_0;
	// System.Int32 Interop/Sys/FileStatus::Mode
	int32_t ___Mode_1;
	// System.UInt32 Interop/Sys/FileStatus::Uid
	uint32_t ___Uid_2;
	// System.UInt32 Interop/Sys/FileStatus::Gid
	uint32_t ___Gid_3;
	// System.Int64 Interop/Sys/FileStatus::Size
	int64_t ___Size_4;
	// System.Int64 Interop/Sys/FileStatus::ATime
	int64_t ___ATime_5;
	// System.Int64 Interop/Sys/FileStatus::ATimeNsec
	int64_t ___ATimeNsec_6;
	// System.Int64 Interop/Sys/FileStatus::MTime
	int64_t ___MTime_7;
	// System.Int64 Interop/Sys/FileStatus::MTimeNsec
	int64_t ___MTimeNsec_8;
	// System.Int64 Interop/Sys/FileStatus::CTime
	int64_t ___CTime_9;
	// System.Int64 Interop/Sys/FileStatus::CTimeNsec
	int64_t ___CTimeNsec_10;
	// System.Int64 Interop/Sys/FileStatus::BirthTime
	int64_t ___BirthTime_11;
	// System.Int64 Interop/Sys/FileStatus::BirthTimeNsec
	int64_t ___BirthTimeNsec_12;
	// System.Int64 Interop/Sys/FileStatus::Dev
	int64_t ___Dev_13;
	// System.Int64 Interop/Sys/FileStatus::Ino
	int64_t ___Ino_14;
	// System.UInt32 Interop/Sys/FileStatus::UserFlags
	uint32_t ___UserFlags_15;
};

// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>
struct AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0 
{
	// System.Runtime.CompilerServices.AsyncMethodBuilderCore System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::m_coreState
	AsyncMethodBuilderCore_tD5ABB3A2536319A3345B32A5481E37E23DD8CEDF ___m_coreState_1;
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::m_task
	Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* ___m_task_2;
};

struct AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0_StaticFields
{
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::s_defaultResultTask
	Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* ___s_defaultResultTask_0;
};

// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>
struct AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F 
{
	// System.Runtime.CompilerServices.AsyncMethodBuilderCore System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::m_coreState
	AsyncMethodBuilderCore_tD5ABB3A2536319A3345B32A5481E37E23DD8CEDF ___m_coreState_1;
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::m_task
	Task_1_t95921EB64E237ACD28589D64B693C652268F225E* ___m_task_2;
};

struct AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_StaticFields
{
	// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1::s_defaultResultTask
	Task_1_t95921EB64E237ACD28589D64B693C652268F225E* ___s_defaultResultTask_0;
};

// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D  : public RuntimeObject
{
	// UnityEngine.AndroidJavaClass UnityEngine.AndroidJavaProxy::javaInterface
	AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* ___javaInterface_0;
	// System.IntPtr UnityEngine.AndroidJavaProxy::proxyObject
	intptr_t ___proxyObject_1;
};

struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_StaticFields
{
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaProxy::s_JavaLangSystemClass
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___s_JavaLangSystemClass_2;
	// System.IntPtr UnityEngine.AndroidJavaProxy::s_HashCodeMethodID
	intptr_t ___s_HashCodeMethodID_3;
};

// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.IntPtr UnityEngine.AsyncOperation::m_Ptr
	intptr_t ___m_Ptr_0;
	// System.Action`1<UnityEngine.AsyncOperation> UnityEngine.AsyncOperation::m_completeCallback
	Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* ___m_completeCallback_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};
// Native definition for COM marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};

// UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.CertificateHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.CertificateHandler
struct CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.DownloadHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.DownloadHandler
struct DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};

struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// System.IO.FileStatus
struct FileStatus_tABB5F252F1E597EC95E9041035DC424EF66712A5 
{
	// Interop/Sys/FileStatus System.IO.FileStatus::_fileStatus
	FileStatus_tCB96EDE0D0F945F685B9BBED6DBF0731207458C2 ____fileStatus_0;
	// System.Int32 System.IO.FileStatus::_fileStatusInitialized
	int32_t ____fileStatusInitialized_1;
	// System.Boolean System.IO.FileStatus::<InitiallyDirectory>k__BackingField
	bool ___U3CInitiallyDirectoryU3Ek__BackingField_2;
	// System.Boolean System.IO.FileStatus::_isDirectory
	bool ____isDirectory_3;
	// System.Boolean System.IO.FileStatus::_exists
	bool ____exists_4;
};
// Native definition for P/Invoke marshalling of System.IO.FileStatus
struct FileStatus_tABB5F252F1E597EC95E9041035DC424EF66712A5_marshaled_pinvoke
{
	FileStatus_tCB96EDE0D0F945F685B9BBED6DBF0731207458C2 ____fileStatus_0;
	int32_t ____fileStatusInitialized_1;
	int32_t ___U3CInitiallyDirectoryU3Ek__BackingField_2;
	int32_t ____isDirectory_3;
	int32_t ____exists_4;
};
// Native definition for COM marshalling of System.IO.FileStatus
struct FileStatus_tABB5F252F1E597EC95E9041035DC424EF66712A5_marshaled_com
{
	FileStatus_tCB96EDE0D0F945F685B9BBED6DBF0731207458C2 ____fileStatus_0;
	int32_t ____fileStatusInitialized_1;
	int32_t ___U3CInitiallyDirectoryU3Ek__BackingField_2;
	int32_t ____isDirectory_3;
	int32_t ____exists_4;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};

struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.UploadHandler::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Networking.UploadHandler
struct UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.IO.FileSystemInfo
struct FileSystemInfo_tE3063B9229F46B05A5F6D018C8C4CA510104E8E9  : public MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE
{
	// System.IO.FileStatus System.IO.FileSystemInfo::_fileStatus
	FileStatus_tABB5F252F1E597EC95E9041035DC424EF66712A5 ____fileStatus_1;
	// System.String System.IO.FileSystemInfo::FullPath
	String_t* ___FullPath_2;
	// System.String System.IO.FileSystemInfo::OriginalPath
	String_t* ___OriginalPath_3;
	// System.String System.IO.FileSystemInfo::_name
	String_t* ____name_4;
};

// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// NativeCameraNamespace.NCCameraCallbackAndroid
struct NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378  : public AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D
{
	// NativeCamera/CameraCallback NativeCameraNamespace.NCCameraCallbackAndroid::callback
	CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* ___callback_4;
	// NativeCameraNamespace.NCCallbackHelper NativeCameraNamespace.NCCameraCallbackAndroid::callbackHelper
	NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* ___callbackHelper_5;
};

// NativeCameraNamespace.NCPermissionCallbackAndroid
struct NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91  : public AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D
{
	// System.Object NativeCameraNamespace.NCPermissionCallbackAndroid::threadLock
	RuntimeObject* ___threadLock_4;
	// System.Int32 NativeCameraNamespace.NCPermissionCallbackAndroid::<Result>k__BackingField
	int32_t ___U3CResultU3Ek__BackingField_5;
};

// NativeCameraNamespace.NCPermissionCallbackAsyncAndroid
struct NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B  : public AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D
{
	// NativeCamera/PermissionCallback NativeCameraNamespace.NCPermissionCallbackAsyncAndroid::callback
	PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* ___callback_4;
	// NativeCameraNamespace.NCCallbackHelper NativeCameraNamespace.NCPermissionCallbackAsyncAndroid::callbackHelper
	NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* ___callbackHelper_5;
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// UnityEngine.Texture
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700_StaticFields
{
	// System.Int32 UnityEngine.Texture::GenerateAllMips
	int32_t ___GenerateAllMips_4;
};

// UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Networking.UnityWebRequest::m_Ptr
	intptr_t ___m_Ptr_0;
	// UnityEngine.Networking.DownloadHandler UnityEngine.Networking.UnityWebRequest::m_DownloadHandler
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB* ___m_DownloadHandler_1;
	// UnityEngine.Networking.UploadHandler UnityEngine.Networking.UnityWebRequest::m_UploadHandler
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6* ___m_UploadHandler_2;
	// UnityEngine.Networking.CertificateHandler UnityEngine.Networking.UnityWebRequest::m_CertificateHandler
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804* ___m_CertificateHandler_3;
	// System.Uri UnityEngine.Networking.UnityWebRequest::m_Uri
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeCertificateHandlerOnDispose>k__BackingField
	bool ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeDownloadHandlerOnDispose>k__BackingField
	bool ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	// System.Boolean UnityEngine.Networking.UnityWebRequest::<disposeUploadHandlerOnDispose>k__BackingField
	bool ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_pinvoke ___m_DownloadHandler_1;
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_pinvoke ___m_UploadHandler_2;
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_pinvoke ___m_CertificateHandler_3;
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	int32_t ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	int32_t ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	int32_t ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};
// Native definition for COM marshalling of UnityEngine.Networking.UnityWebRequest
struct UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com
{
	intptr_t ___m_Ptr_0;
	DownloadHandler_t1B56C7D3F65D97A1E4B566A14A1E783EA8AE4EBB_marshaled_com* ___m_DownloadHandler_1;
	UploadHandler_t7E504B1A83346248A0C8C4AF73A893226CB83EF6_marshaled_com* ___m_UploadHandler_2;
	CertificateHandler_t148B524FA5DB39F3ABADB181CD420FC505C33804_marshaled_com* ___m_CertificateHandler_3;
	Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E* ___m_Uri_4;
	int32_t ___U3CdisposeCertificateHandlerOnDisposeU3Ek__BackingField_5;
	int32_t ___U3CdisposeDownloadHandlerOnDisposeU3Ek__BackingField_6;
	int32_t ___U3CdisposeUploadHandlerOnDisposeU3Ek__BackingField_7;
};

// UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
	// UnityEngine.Networking.UnityWebRequest UnityEngine.Networking.UnityWebRequestAsyncOperation::<webRequest>k__BackingField
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* ___U3CwebRequestU3Ek__BackingField_2;
};
// Native definition for P/Invoke marshalling of UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_pinvoke* ___U3CwebRequestU3Ek__BackingField_2;
};
// Native definition for COM marshalling of UnityEngine.Networking.UnityWebRequestAsyncOperation
struct UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
{
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F_marshaled_com* ___U3CwebRequestU3Ek__BackingField_2;
};

// NativeCamera/<GetVideoThumbnailAsync>d__30
struct U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0 
{
	// System.Int32 NativeCamera/<GetVideoThumbnailAsync>d__30::<>1__state
	int32_t ___U3CU3E1__state_0;
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D> NativeCamera/<GetVideoThumbnailAsync>d__30::<>t__builder
	AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F ___U3CU3Et__builder_1;
	// System.String NativeCamera/<GetVideoThumbnailAsync>d__30::videoPath
	String_t* ___videoPath_2;
	// System.Int32 NativeCamera/<GetVideoThumbnailAsync>d__30::maxSize
	int32_t ___maxSize_3;
	// System.Double NativeCamera/<GetVideoThumbnailAsync>d__30::captureTimeInSeconds
	double ___captureTimeInSeconds_4;
	// NativeCamera/<>c__DisplayClass30_0 NativeCamera/<GetVideoThumbnailAsync>d__30::<>8__1
	U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* ___U3CU3E8__1_5;
	// System.Boolean NativeCamera/<GetVideoThumbnailAsync>d__30::markTextureNonReadable
	bool ___markTextureNonReadable_6;
	// System.Runtime.CompilerServices.TaskAwaiter`1<System.String> NativeCamera/<GetVideoThumbnailAsync>d__30::<>u__1
	TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 ___U3CU3Eu__1_7;
	// System.Runtime.CompilerServices.TaskAwaiter`1<UnityEngine.Texture2D> NativeCamera/<GetVideoThumbnailAsync>d__30::<>u__2
	TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B ___U3CU3Eu__2_8;
};

// NativeCamera/<LoadImageAtPathAsync>d__28
struct U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9 
{
	// System.Int32 NativeCamera/<LoadImageAtPathAsync>d__28::<>1__state
	int32_t ___U3CU3E1__state_0;
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D> NativeCamera/<LoadImageAtPathAsync>d__28::<>t__builder
	AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F ___U3CU3Et__builder_1;
	// System.String NativeCamera/<LoadImageAtPathAsync>d__28::imagePath
	String_t* ___imagePath_2;
	// System.Int32 NativeCamera/<LoadImageAtPathAsync>d__28::maxSize
	int32_t ___maxSize_3;
	// System.Boolean NativeCamera/<LoadImageAtPathAsync>d__28::markTextureNonReadable
	bool ___markTextureNonReadable_4;
	// NativeCamera/<>c__DisplayClass28_0 NativeCamera/<LoadImageAtPathAsync>d__28::<>8__1
	U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* ___U3CU3E8__1_5;
	// System.String NativeCamera/<LoadImageAtPathAsync>d__28::<loadPath>5__2
	String_t* ___U3CloadPathU3E5__2_6;
	// UnityEngine.Texture2D NativeCamera/<LoadImageAtPathAsync>d__28::<result>5__3
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___U3CresultU3E5__3_7;
	// System.Runtime.CompilerServices.TaskAwaiter`1<System.String> NativeCamera/<LoadImageAtPathAsync>d__28::<>u__1
	TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 ___U3CU3Eu__1_8;
	// UnityEngine.Networking.UnityWebRequest NativeCamera/<LoadImageAtPathAsync>d__28::<www>5__4
	UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* ___U3CwwwU3E5__4_9;
	// UnityEngine.Networking.UnityWebRequestAsyncOperation NativeCamera/<LoadImageAtPathAsync>d__28::<asyncOperation>5__5
	UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* ___U3CasyncOperationU3E5__5_10;
	// System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter NativeCamera/<LoadImageAtPathAsync>d__28::<>u__2
	YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A ___U3CU3Eu__2_11;
};

// System.Func`1<System.String>
struct Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C  : public MulticastDelegate_t
{
};

// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};

// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	// System.String System.ArgumentException::_paramName
	String_t* ____paramName_18;
};

// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// System.IO.DirectoryInfo
struct DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2  : public FileSystemInfo_tE3063B9229F46B05A5F6D018C8C4CA510104E8E9
{
};

// System.IO.IOException
struct IOException_t5D599190B003D41D45D4839A9B6B9AB53A755910  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4  : public Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700
{
};

// NativeCamera/CameraCallback
struct CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771  : public MulticastDelegate_t
{
};

// NativeCamera/PermissionCallback
struct PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001  : public MulticastDelegate_t
{
};

// System.IO.FileNotFoundException
struct FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A  : public IOException_t5D599190B003D41D45D4839A9B6B9AB53A755910
{
	// System.String System.IO.FileNotFoundException::<FileName>k__BackingField
	String_t* ___U3CFileNameU3Ek__BackingField_18;
	// System.String System.IO.FileNotFoundException::<FusionLog>k__BackingField
	String_t* ___U3CFusionLogU3Ek__BackingField_19;
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// NativeCameraNamespace.NCCallbackHelper
struct NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// System.Action NativeCameraNamespace.NCCallbackHelper::mainThreadAction
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___mainThreadAction_4;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// FieldType UnityEngine.AndroidJavaObject::GetStatic<System.Object>(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObject_GetStatic_TisRuntimeObject_m4EF4E4761A0A6E99E0A298F653E8129B1494E4C9_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___fieldName0, const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Int32>(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.TaskCompletionSource`1<System.Int32Enum>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TaskCompletionSource_1__ctor_m9FD8C5F5BF7CC119D6FF330BA42C60284DB47E65_gshared (TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A* __this, const RuntimeMethod* method) ;
// System.Threading.Tasks.Task`1<TResult> System.Threading.Tasks.TaskCompletionSource`1<System.Int32Enum>::get_Task()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB* TaskCompletionSource_1_get_Task_mB4A2FF75AC28BB6E3B7A55129E9CD347E5F06FDC_gshared_inline (TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A* __this, const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Boolean>(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Object>(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObject_CallStatic_TisRuntimeObject_mCAFE27630F6092C4910E14592B050DACFCBE146F_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::Create()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0 AsyncTaskMethodBuilder_1_Create_m6A59453D00C0143F178809ADFD98C90E8C291ABB_gshared (const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::Start<NativeCamera/<LoadImageAtPathAsync>d__28>(TStateMachine&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m68AEF628EEA88B9A47C135CC0B0D5490F50419FD_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine0, const RuntimeMethod* method) ;
// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::get_Task()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* AsyncTaskMethodBuilder_1_get_Task_mEA092EC6F1324A9D694CF6056FA8583F2A2BDC89_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::Start<NativeCamera/<GetVideoThumbnailAsync>d__30>(TStateMachine&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_mBC231C71460E6D9F479BB8A7606F05923ACA0BF9_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* ___stateMachine0, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.TaskCompletionSource`1<System.Int32Enum>::SetResult(TResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TaskCompletionSource_1_SetResult_m9AEEE70A249C411C5DE39635EF35DAF9D5FAF458_gshared (TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A* __this, int32_t ___result0, const RuntimeMethod* method) ;
// System.Void System.Func`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_1__ctor_m663374A863E492A515BE9626B6F0E444991834E8_gshared (Func_1_tD5C081AE11746B200C711DD48DBEB00E3A9276D4* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Threading.Tasks.Task`1<T> NativeCamera::TryCallNativeAndroidFunctionOnSeparateThread<System.Object>(System.Func`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisRuntimeObject_mEE72D71E5EAD74CF3DC87613CBDEDE28166920DD_gshared (Func_1_tD5C081AE11746B200C711DD48DBEB00E3A9276D4* ___function0, const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.TaskAwaiter`1<TResult> System.Threading.Tasks.Task`1<System.Object>::GetAwaiter()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA Task_1_GetAwaiter_mD80ED263BF3F1F8DBDBD177BA3401A0AAAFA38E3_gshared (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* __this, const RuntimeMethod* method) ;
// System.Boolean System.Runtime.CompilerServices.TaskAwaiter`1<System.Object>::get_IsCompleted()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TaskAwaiter_1_get_IsCompleted_mEEBB09E26F4165A0F864D92E1890CFCD2C8CFD54_gshared (TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter`1<System.Object>,NativeCamera/<LoadImageAtPathAsync>d__28>(TAwaiter&,TStateMachine&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m4C61C9BC605A10331CC647EAA21D6C71A2D46E65_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA* ___awaiter0, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine1, const RuntimeMethod* method) ;
// TResult System.Runtime.CompilerServices.TaskAwaiter`1<System.Object>::GetResult()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* TaskAwaiter_1_GetResult_mA4A8A1F43A456B40DDA251D00026C60919AED85B_gshared (TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter,NativeCamera/<LoadImageAtPathAsync>d__28>(TAwaiter&,TStateMachine&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m99BD8C46583B19B483BD65F96417215FCB9D53ED_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A* ___awaiter0, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine1, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::SetException(System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_SetException_mC2F74B26F5303F9F960965220E2866D777F1A5C6_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, Exception_t* ___exception0, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::SetResult(TResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_SetResult_m0D83195F995F9825D7A6DCDC3835D6917C43B5A6_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, RuntimeObject* ___result0, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_SetStateMachine_m3BE54983634ABF5BE05200C7894AD0F9F20BDD65_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Object>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter`1<System.Object>,NativeCamera/<GetVideoThumbnailAsync>d__30>(TAwaiter&,TStateMachine&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m1C5D50DB1E9D3825F2C05237659D7DD0E83DB28F_gshared (AsyncTaskMethodBuilder_1_tE810F083929D7952F192036D298085BD4B048AD0* __this, TaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA* ___awaiter0, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* ___stateMachine1, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;

// System.Void UnityEngine.AndroidJavaClass::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389 (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* __this, String_t* ___className0, const RuntimeMethod* method) ;
// FieldType UnityEngine.AndroidJavaObject::GetStatic<UnityEngine.AndroidJavaObject>(System.String)
inline AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___fieldName0, const RuntimeMethod* method)
{
	return ((  AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, const RuntimeMethod*))AndroidJavaObject_GetStatic_TisRuntimeObject_m4EF4E4761A0A6E99E0A298F653E8129B1494E4C9_gshared)(__this, ___fieldName0, method);
}
// System.String UnityEngine.Application::get_temporaryCachePath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Application_get_temporaryCachePath_mE4483A939988E69570C19F8B31AB9FB17B0FD97D (const RuntimeMethod* method) ;
// System.String System.IO.Path::Combine(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Path_Combine_m1ADAC05CDA2D1D61B172DF65A81E86592696BEAE (String_t* ___path10, String_t* ___path21, const RuntimeMethod* method) ;
// System.IO.DirectoryInfo System.IO.Directory::CreateDirectory(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2* Directory_CreateDirectory_m16EC5CE8561A997C6635E06DC24C77590F29D94F (String_t* ___path0, const RuntimeMethod* method) ;
// UnityEngine.AndroidJavaClass NativeCamera::get_AJC()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E (const RuntimeMethod* method) ;
// UnityEngine.AndroidJavaObject NativeCamera::get_Context()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD (const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Int32>(System.String,System.Object[])
inline int32_t AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method)
{
	return ((  int32_t (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A_gshared)(__this, ___methodName0, ___args1, method);
}
// System.Int32 UnityEngine.PlayerPrefs::GetInt(System.String,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t PlayerPrefs_GetInt_m8AD1FA8BA54CC6CE2B2AEEE36B6D75587BB1692D (String_t* ___key0, int32_t ___defaultValue1, const RuntimeMethod* method) ;
// NativeCamera/Permission NativeCamera::CheckPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_CheckPermission_m0445839C87DFBA62F088F4A2116FFA53173470CD (bool ___isPicturePermission0, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Monitor::Exit(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA (RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Void System.Threading.Monitor::Enter(System.Object,System.Boolean&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149 (RuntimeObject* ___obj0, bool* ___lockTaken1, const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCPermissionCallbackAndroid::.ctor(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid__ctor_mEE399CA43702611369BBFD6E72E3147E4C962FDC (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, RuntimeObject* ___threadLock0, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaObject::CallStatic(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// System.Int32 NativeCameraNamespace.NCPermissionCallbackAndroid::get_Result()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Monitor::Wait(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Monitor_Wait_m322138959FFE3F4F3212658ACB0C30C981880D28 (RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Void UnityEngine.PlayerPrefs::SetInt(System.String,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayerPrefs_SetInt_m956D3E2DB966F20CF42F842880DDF9E2BE94D948 (String_t* ___key0, int32_t ___value1, const RuntimeMethod* method) ;
// System.Void UnityEngine.PlayerPrefs::Save()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayerPrefs_Save_m82567E045D69C838112EA204B60C144D4C1EA3AE (const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid::.ctor(NativeCamera/PermissionCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAsyncAndroid__ctor_m148D3C8505CACDED882A222F2081610060ACF67F (NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* __this, PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* ___callback0, const RuntimeMethod* method) ;
// System.Void NativeCamera/<>c__DisplayClass20_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass20_0__ctor_mFBDEA24DAAE658B4CB10A30CA2C7BE3D5F8F8655 (U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission>::.ctor()
inline void TaskCompletionSource_1__ctor_m59FEBA96E2C649BB6B9E4A681806C7ED628834FB (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* __this, const RuntimeMethod* method)
{
	((  void (*) (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531*, const RuntimeMethod*))TaskCompletionSource_1__ctor_m9FD8C5F5BF7CC119D6FF330BA42C60284DB47E65_gshared)(__this, method);
}
// System.Void NativeCamera/PermissionCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallback__ctor_m7CA919F20741081C2F81A209707B998CCC4D3782 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void NativeCamera::RequestPermissionAsync(NativeCamera/PermissionCallback,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeCamera_RequestPermissionAsync_mC8600A67BB4F1B65AB202009AA15E8A1406395F3 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* ___callback0, bool ___isPicturePermission1, const RuntimeMethod* method) ;
// System.Threading.Tasks.Task`1<TResult> System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission>::get_Task()
inline Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED* TaskCompletionSource_1_get_Task_mDCD7F80204B3108705062BD8783CD541E08A4157_inline (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* __this, const RuntimeMethod* method)
{
	return ((  Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED* (*) (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531*, const RuntimeMethod*))TaskCompletionSource_1_get_Task_mB4A2FF75AC28BB6E3B7A55129E9CD347E5F06FDC_gshared_inline)(__this, method);
}
// NativeCamera/Permission NativeCamera::RequestPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_RequestPermission_m22C7DBA897A6DB77BFB1C2DF886FC1F331CFEDB1 (bool ___isPicturePermission0, const RuntimeMethod* method) ;
// System.Boolean NativeCamera::IsCameraBusy()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeCamera_IsCameraBusy_m3373F1A16F70303B7F53D47A1143BED4152C2F54 (const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid::.ctor(NativeCamera/CameraCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCameraCallbackAndroid__ctor_m7A49A723EBFBFC7215432493733AFCF632A8F5AA (NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* __this, CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* ___callback0, const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Boolean>(System.String,System.Object[])
inline bool AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method)
{
	return ((  bool (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6_gshared)(__this, ___methodName0, ___args1, method);
}
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478 (String_t* ___value0, const RuntimeMethod* method) ;
// System.Void System.ArgumentException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Boolean System.IO.File::Exists(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A (String_t* ___path0, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___str00, String_t* ___str11, const RuntimeMethod* method) ;
// System.Void System.IO.FileNotFoundException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FileNotFoundException__ctor_mA8C9C93DB8C5B96D6B5E59B2AE07154F265FB1A1 (FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.SystemInfo::get_maxTextureSize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SystemInfo_get_maxTextureSize_mEE557C09643222591C6F4D3F561D7A60CD403991 (const RuntimeMethod* method) ;
// System.String NativeCamera::get_TemporaryImagePath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027 (const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.String>(System.String,System.Object[])
inline String_t* AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___methodName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method)
{
	return ((  String_t* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject_CallStatic_TisRuntimeObject_mCAFE27630F6092C4910E14592B050DACFCBE146F_gshared)(__this, ___methodName0, ___args1, method);
}
// System.String System.IO.Path::GetExtension(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Path_GetExtension_m6FEAA9E14451BFD210B9D1AEC2430C813F570FE5 (String_t* ___path0, const RuntimeMethod* method) ;
// System.String System.String::ToLowerInvariant()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_ToLowerInvariant_mBE32C93DE27C5353FEA3FA654FC1DDBE3D0EB0F2 (String_t* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Void UnityEngine.Texture2D::.ctor(System.Int32,System.Int32,UnityEngine.TextureFormat,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Texture2D__ctor_mC3F84195D1DCEFC0536B3FBD40A8F8E865A4F32A (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* __this, int32_t ___width0, int32_t ___height1, int32_t ___textureFormat2, bool ___mipChain3, bool ___linear4, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Inequality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Inequality_m8C940F3CFC42866709D7CA931B3D77B4BE94BCB6 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Void System.IO.File::Delete(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void File_Delete_mE29829DA504F3E1B8BCB78F21E2862C9ED7EC386 (String_t* ___path0, const RuntimeMethod* method) ;
// System.Byte[] System.IO.File::ReadAllBytes(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* File_ReadAllBytes_m704CBBA3F130C94F5A3E0BE2A93D9E9D79DC3E24 (String_t* ___path0, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.ImageConversion::LoadImage(UnityEngine.Texture2D,System.Byte[],System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ImageConversion_LoadImage_m292ADCEED268A0A0AAD532BAB8D1710CF0FC8AEF (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___tex0, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___data1, bool ___markNonReadable2, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogWarning(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___message0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::DestroyImmediate(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_DestroyImmediate_m6336EBC83591A5DB64EC70C92132824C6E258705 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___obj0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogException(System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogException_mAB3F4DC7297ED8FBB49DAA718B70E59A6B0171B0 (Exception_t* ___exception0, const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::Create()
inline AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4 (const RuntimeMethod* method)
{
	return ((  AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F (*) (const RuntimeMethod*))AsyncTaskMethodBuilder_1_Create_m6A59453D00C0143F178809ADFD98C90E8C291ABB_gshared)(method);
}
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::Start<NativeCamera/<LoadImageAtPathAsync>d__28>(TStateMachine&)
inline void AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mC08662C58644EB7E11271EF5EDFB23D1678CB978 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine0, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m68AEF628EEA88B9A47C135CC0B0D5490F50419FD_gshared)(__this, ___stateMachine0, method);
}
// System.Threading.Tasks.Task`1<TResult> System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::get_Task()
inline Task_1_t95921EB64E237ACD28589D64B693C652268F225E* AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, const RuntimeMethod* method)
{
	return ((  Task_1_t95921EB64E237ACD28589D64B693C652268F225E* (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_get_Task_mEA092EC6F1324A9D694CF6056FA8583F2A2BDC89_gshared)(__this, method);
}
// UnityEngine.Texture2D NativeCamera::LoadImageAtPath(System.String,System.Int32,System.Boolean,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747 (String_t* ___imagePath0, int32_t ___maxSize1, bool ___markTextureNonReadable2, bool ___generateMipmaps3, bool ___linearColorSpace4, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::Start<NativeCamera/<GetVideoThumbnailAsync>d__30>(TStateMachine&)
inline void AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m59F56DB7F0E99D8EA793C64E8735398C0539DBE6 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* ___stateMachine0, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_mBC231C71460E6D9F479BB8A7606F05923ACA0BF9_gshared)(__this, ___stateMachine0, method);
}
// System.String[] System.String::Split(System.Char,System.StringSplitOptions)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* String_Split_m9530B73D02054692283BF35C3A27C8F2230946F4 (String_t* __this, Il2CppChar ___separator0, int32_t ___options1, const RuntimeMethod* method) ;
// System.String System.String::Trim()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5 (String_t* __this, const RuntimeMethod* method) ;
// System.Boolean System.Int32::TryParse(System.String,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21 (String_t* ___s0, int32_t* ___result1, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
// System.Void NativeCamera/ImageProperties::.ctor(System.Int32,System.Int32,System.String,NativeCamera/ImageOrientation)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ImageProperties__ctor_mDB1459622F0FFEEA741B63898D7CE5B41AA4F9EC (ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46* __this, int32_t ___width0, int32_t ___height1, String_t* ___mimeType2, int32_t ___orientation3, const RuntimeMethod* method) ;
// System.Boolean System.Int64::TryParse(System.String,System.Int64&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Int64_TryParse_m3FC0128C89CC2331239FC2A0A749BF33455F03D2 (String_t* ___s0, int64_t* ___result1, const RuntimeMethod* method) ;
// System.String System.String::Replace(System.Char,System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Replace_m86403DC5F422D8D5E1CFAAF255B103CB807EDAAF (String_t* __this, Il2CppChar ___oldChar0, Il2CppChar ___newChar1, const RuntimeMethod* method) ;
// System.Globalization.CultureInfo System.Globalization.CultureInfo::get_InvariantCulture()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* CultureInfo_get_InvariantCulture_mD1E96DC845E34B10F78CB744B0CB5D7D63CEB1E6 (const RuntimeMethod* method) ;
// System.Boolean System.Single::TryParse(System.String,System.Globalization.NumberStyles,System.IFormatProvider,System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Single_TryParse_mFB8CC32F0016FBB6EFCB97953CF3515767EB6431 (String_t* ___s0, int32_t ___style1, RuntimeObject* ___provider2, float* ___result3, const RuntimeMethod* method) ;
// System.Void NativeCamera/VideoProperties::.ctor(System.Int32,System.Int32,System.Int64,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VideoProperties__ctor_m61A493A9C32C95FE9B3F84709AC0A2E92283F97D (VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E* __this, int32_t ___width0, int32_t ___height1, int64_t ___duration2, float ___rotation3, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.TaskCompletionSource`1<NativeCamera/Permission>::SetResult(TResult)
inline void TaskCompletionSource_1_SetResult_mE3BD8B51FE6830985D8550B8E7F71C413B087604 (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* __this, int32_t ___result0, const RuntimeMethod* method)
{
	((  void (*) (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531*, int32_t, const RuntimeMethod*))TaskCompletionSource_1_SetResult_m9AEEE70A249C411C5DE39635EF35DAF9D5FAF458_gshared)(__this, ___result0, method);
}
// System.Void NativeCamera/<>c__DisplayClass28_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass28_0__ctor_mF21CA894004F6525050D93D9D5332A514261D347 (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* __this, const RuntimeMethod* method) ;
// System.Void System.Func`1<System.String>::.ctor(System.Object,System.IntPtr)
inline void Func_1__ctor_m27A68E928C1D9158EAAD261086B9BC424339327B (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C*, RuntimeObject*, intptr_t, const RuntimeMethod*))Func_1__ctor_m663374A863E492A515BE9626B6F0E444991834E8_gshared)(__this, ___object0, ___method1, method);
}
// System.Threading.Tasks.Task`1<T> NativeCamera::TryCallNativeAndroidFunctionOnSeparateThread<System.String>(System.Func`1<T>)
inline Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C* ___function0, const RuntimeMethod* method)
{
	return ((  Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* (*) (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C*, const RuntimeMethod*))NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisRuntimeObject_mEE72D71E5EAD74CF3DC87613CBDEDE28166920DD_gshared)(___function0, method);
}
// System.Runtime.CompilerServices.TaskAwaiter`1<TResult> System.Threading.Tasks.Task`1<System.String>::GetAwaiter()
inline TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F (Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* __this, const RuntimeMethod* method)
{
	return ((  TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 (*) (Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0*, const RuntimeMethod*))Task_1_GetAwaiter_mD80ED263BF3F1F8DBDBD177BA3401A0AAAFA38E3_gshared)(__this, method);
}
// System.Boolean System.Runtime.CompilerServices.TaskAwaiter`1<System.String>::get_IsCompleted()
inline bool TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8 (TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6*, const RuntimeMethod*))TaskAwaiter_1_get_IsCompleted_mEEBB09E26F4165A0F864D92E1890CFCD2C8CFD54_gshared)(__this, method);
}
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter`1<System.String>,NativeCamera/<LoadImageAtPathAsync>d__28>(TAwaiter&,TStateMachine&)
inline void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mA065D9BD25C55B7B2630CA549183C4615FF27DFC (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* ___awaiter0, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine1, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6*, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m4C61C9BC605A10331CC647EAA21D6C71A2D46E65_gshared)(__this, ___awaiter0, ___stateMachine1, method);
}
// TResult System.Runtime.CompilerServices.TaskAwaiter`1<System.String>::GetResult()
inline String_t* TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9 (TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* __this, const RuntimeMethod* method)
{
	return ((  String_t* (*) (TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6*, const RuntimeMethod*))TaskAwaiter_1_GetResult_mA4A8A1F43A456B40DDA251D00026C60919AED85B_gshared)(__this, method);
}
// UnityEngine.Networking.UnityWebRequest UnityEngine.Networking.UnityWebRequestTexture::GetTexture(System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* UnityWebRequestTexture_GetTexture_m45F855106C834021AC0DFA25FE31BA14C42693CA (String_t* ___uri0, bool ___nonReadable1, const RuntimeMethod* method) ;
// UnityEngine.Networking.UnityWebRequestAsyncOperation UnityEngine.Networking.UnityWebRequest::SendWebRequest()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* UnityWebRequest_SendWebRequest_mA3CD13983BAA5074A0640EDD661B1E46E6DB6C13 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.YieldAwaitable System.Threading.Tasks.Task::Yield()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB Task_Yield_m27EE257EF53788244C5B2E874C514C24C693F9B1 (const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter System.Runtime.CompilerServices.YieldAwaitable::GetAwaiter()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A YieldAwaitable_GetAwaiter_m359A05B8C1B9F3F1E9CAE29AD231C0987718DE5E (YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB* __this, const RuntimeMethod* method) ;
// System.Boolean System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::get_IsCompleted()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool YieldAwaiter_get_IsCompleted_m783B6E67654FDBF490A65AC59972AF6B985A9286 (YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter,NativeCamera/<LoadImageAtPathAsync>d__28>(TAwaiter&,TStateMachine&)
inline void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m56EC0B7A46A5E522CCEAB37456060C36E26CE731 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A* ___awaiter0, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* ___stateMachine1, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A*, U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m99BD8C46583B19B483BD65F96417215FCB9D53ED_gshared)(__this, ___awaiter0, ___stateMachine1, method);
}
// System.Void System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::GetResult()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YieldAwaiter_GetResult_m83C9B35D4BBEB09AC5B560912436454D69794F07 (YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.AsyncOperation::get_isDone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AsyncOperation_get_isDone_m68A0682777E2132FC033182E9F50303566AA354D (AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C* __this, const RuntimeMethod* method) ;
// UnityEngine.Networking.UnityWebRequest/Result UnityEngine.Networking.UnityWebRequest::get_result()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t UnityWebRequest_get_result_mEF83848C5FCFB5E307CE4B57E42BF02FC9AED449 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Networking.UnityWebRequest::get_error()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UnityWebRequest_get_error_m20A5D813ED59118B7AA1D1E2EB5250178B1F5B6F (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* __this, const RuntimeMethod* method) ;
// UnityEngine.Texture2D UnityEngine.Networking.DownloadHandlerTexture::GetContent(UnityEngine.Networking.UnityWebRequest)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* DownloadHandlerTexture_GetContent_m86BC88F58305A1B21C21CE7D82658197932EFB18 (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* ___www0, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Implicit(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___exists0, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::SetException(System.Exception)
inline void AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, Exception_t* ___exception0, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, Exception_t*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_SetException_mC2F74B26F5303F9F960965220E2866D777F1A5C6_gshared)(__this, ___exception0, method);
}
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::SetResult(TResult)
inline void AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___result0, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_SetResult_m0D83195F995F9825D7A6DCDC3835D6917C43B5A6_gshared)(__this, ___result0, method);
}
// System.Void NativeCamera/<LoadImageAtPathAsync>d__28::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B (U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
inline void AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, RuntimeObject*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_SetStateMachine_m3BE54983634ABF5BE05200C7894AD0F9F20BDD65_gshared)(__this, ___stateMachine0, method);
}
// System.Void NativeCamera/<LoadImageAtPathAsync>d__28::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadImageAtPathAsyncU3Ed__28_SetStateMachine_mCC03A217715E97CF709DD6F9646EE16946B331B1 (U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method) ;
// System.Void NativeCamera/<>c__DisplayClass30_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass30_0__ctor_m1FE9FD5C2A2C59BAB3B1BD833EE7F70A5D49DACE (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter`1<System.String>,NativeCamera/<GetVideoThumbnailAsync>d__30>(TAwaiter&,TStateMachine&)
inline void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m4B4C82CEEADEF9B96CFD0819E9DA9DC931389FE4 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* ___awaiter0, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* ___stateMachine1, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6*, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m1C5D50DB1E9D3825F2C05237659D7DD0E83DB28F_gshared)(__this, ___awaiter0, ___stateMachine1, method);
}
// System.Threading.Tasks.Task`1<UnityEngine.Texture2D> NativeCamera::LoadImageAtPathAsync(System.String,System.Int32,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t95921EB64E237ACD28589D64B693C652268F225E* NativeCamera_LoadImageAtPathAsync_m7B912F60F0DF9C36558976E4F53701CF76073FC0 (String_t* ___imagePath0, int32_t ___maxSize1, bool ___markTextureNonReadable2, const RuntimeMethod* method) ;
// System.Runtime.CompilerServices.TaskAwaiter`1<TResult> System.Threading.Tasks.Task`1<UnityEngine.Texture2D>::GetAwaiter()
inline TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B Task_1_GetAwaiter_m88AFED53B032F7EDDB6F9746699970B9FFFC992C (Task_1_t95921EB64E237ACD28589D64B693C652268F225E* __this, const RuntimeMethod* method)
{
	return ((  TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B (*) (Task_1_t95921EB64E237ACD28589D64B693C652268F225E*, const RuntimeMethod*))Task_1_GetAwaiter_mD80ED263BF3F1F8DBDBD177BA3401A0AAAFA38E3_gshared)(__this, method);
}
// System.Boolean System.Runtime.CompilerServices.TaskAwaiter`1<UnityEngine.Texture2D>::get_IsCompleted()
inline bool TaskAwaiter_1_get_IsCompleted_m77FF413EE49A5859C0BC111104448D64F3C01911 (TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B*, const RuntimeMethod*))TaskAwaiter_1_get_IsCompleted_mEEBB09E26F4165A0F864D92E1890CFCD2C8CFD54_gshared)(__this, method);
}
// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<UnityEngine.Texture2D>::AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter`1<UnityEngine.Texture2D>,NativeCamera/<GetVideoThumbnailAsync>d__30>(TAwaiter&,TStateMachine&)
inline void AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m850BD17D7C2BD7A37C5B5261B69E44A451533191 (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* __this, TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B* ___awaiter0, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* ___stateMachine1, const RuntimeMethod* method)
{
	((  void (*) (AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F*, TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B*, U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0*, const RuntimeMethod*))AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t0B808409CD8201F13AAC85F29D646518C4857BEA_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m1C5D50DB1E9D3825F2C05237659D7DD0E83DB28F_gshared)(__this, ___awaiter0, ___stateMachine1, method);
}
// TResult System.Runtime.CompilerServices.TaskAwaiter`1<UnityEngine.Texture2D>::GetResult()
inline Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* TaskAwaiter_1_GetResult_mE4B8867B0D8DAA1317AD64FE09FBD26E825A654C (TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B* __this, const RuntimeMethod* method)
{
	return ((  Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* (*) (TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B*, const RuntimeMethod*))TaskAwaiter_1_GetResult_mA4A8A1F43A456B40DDA251D00026C60919AED85B_gshared)(__this, method);
}
// System.Void NativeCamera/<GetVideoThumbnailAsync>d__30::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetVideoThumbnailAsyncU3Ed__30_MoveNext_mDBB890CADFAADFBF703E0257D74428E64FE6FDA6 (U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* __this, const RuntimeMethod* method) ;
// System.Void NativeCamera/<GetVideoThumbnailAsync>d__30::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetVideoThumbnailAsyncU3Ed__30_SetStateMachine_m3D9A0912DB18DF40ACC898395143BBF274B31AEB (U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.Component::get_gameObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::DontDestroyOnLoad(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_DontDestroyOnLoad_m4B70C3AEF886C176543D1295507B6455C9DCAEA7 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___target0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::Destroy(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_Destroy_mE97D0A766419A81296E8D4E5C23D01D3FE91ACBB (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___obj0, const RuntimeMethod* method) ;
// System.Void System.Action::Invoke()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaProxy::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7 (AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D* __this, String_t* ___javaInterface0, const RuntimeMethod* method) ;
// System.Void UnityEngine.GameObject::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject__ctor_m37D512B05D292F954792225E6C6EEE95293A9B88 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, String_t* ___name0, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<NativeCameraNamespace.NCCallbackHelper>()
inline NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0__ctor_mAEE97D12D327C8C28EAB45ACDCB50E5DC04F50FB (U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* __this, const RuntimeMethod* method) ;
// System.Void System.Action::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCCallbackHelper::CallOnMainThread(System.Action)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NCCallbackHelper_CallOnMainThread_mF4C7C68F199FEE8034F6E1EFFE293DB4D92D5192_inline (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___function0, const RuntimeMethod* method) ;
// System.Void NativeCamera/CameraCallback::Invoke(System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_inline (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCPermissionCallbackAndroid::set_Result(System.Int32)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid_set_Result_m875E1D987FD8099207FD71BC8FCCE424DEA1DC6A_inline (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, int32_t ___value0, const RuntimeMethod* method) ;
// System.Void System.Threading.Monitor::Pulse(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Pulse_mA8279943D6C2913ADFAF661E35C4951BDFACE43D (RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0__ctor_mD3F7CC1873EDBE61E4E200563157CFB5895A4563 (U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* __this, const RuntimeMethod* method) ;
// System.Void NativeCamera/PermissionCallback::Invoke(NativeCamera/Permission)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_inline (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.AndroidJavaClass NativeCamera::get_AJC()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral60765CA56083EB814A8E566B08E35D91D1000DEC);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if( m_ajc == null )
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_ajc_0;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		// m_ajc = new AndroidJavaClass( "com.yasirkula.unity.NativeCamera" );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_1 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389(L_1, _stringLiteral60765CA56083EB814A8E566B08E35D91D1000DEC, NULL);
		((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_ajc_0 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_ajc_0), (void*)L_1);
	}

IL_0016:
	{
		// return m_ajc;
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_2 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_ajc_0;
		return L_2;
	}
}
// UnityEngine.AndroidJavaObject NativeCamera::get_Context()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9);
		s_Il2CppMethodInitialized = true;
	}
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* V_0 = NULL;
	{
		// if( m_context == null )
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_0 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_context_1;
		if (L_0)
		{
			goto IL_002e;
		}
	}
	{
		// using( AndroidJavaObject unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_1 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389(L_1, _stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078, NULL);
		V_0 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0024:
			{// begin finally (depth: 1)
				{
					AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_2 = V_0;
					if (!L_2)
					{
						goto IL_002d;
					}
				}
				{
					AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3 = V_0;
					NullCheck(L_3);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_3);
				}

IL_002d:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			// m_context = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" );
			AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_4 = V_0;
			NullCheck(L_4);
			AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5;
			L_5 = AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD(L_4, _stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9, AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var);
			((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_context_1 = L_5;
			Il2CppCodeGenWriteBarrier((void**)(&((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_context_1), (void*)L_5);
			// }
			goto IL_002e;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_002e:
	{
		// return m_context;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_6 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_context_1;
		return L_6;
	}
}
// System.String NativeCamera::get_TemporaryImagePath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1256BD059A8D156C0578EF505C83E5862F0EFCD2);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if( m_temporaryImagePath == null )
		String_t* L_0 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_temporaryImagePath_2;
		if (L_0)
		{
			goto IL_0026;
		}
	}
	{
		// m_temporaryImagePath = Path.Combine( Application.temporaryCachePath, "tmpImg" );
		String_t* L_1;
		L_1 = Application_get_temporaryCachePath_mE4483A939988E69570C19F8B31AB9FB17B0FD97D(NULL);
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_2;
		L_2 = Path_Combine_m1ADAC05CDA2D1D61B172DF65A81E86592696BEAE(L_1, _stringLiteral1256BD059A8D156C0578EF505C83E5862F0EFCD2, NULL);
		((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_temporaryImagePath_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_temporaryImagePath_2), (void*)L_2);
		// Directory.CreateDirectory( Application.temporaryCachePath );
		String_t* L_3;
		L_3 = Application_get_temporaryCachePath_mE4483A939988E69570C19F8B31AB9FB17B0FD97D(NULL);
		DirectoryInfo_tEAEEC018EB49B4A71907FFEAFE935FAA8F9C1FE2* L_4;
		L_4 = Directory_CreateDirectory_m16EC5CE8561A997C6635E06DC24C77590F29D94F(L_3, NULL);
	}

IL_0026:
	{
		// return m_temporaryImagePath;
		String_t* L_5 = ((NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_StaticFields*)il2cpp_codegen_static_fields_for(NativeCamera_tE85F6A2EFD4BDD17480FD3FC273A67753E6C629F_il2cpp_TypeInfo_var))->___m_temporaryImagePath_2;
		return L_5;
	}
}
// NativeCamera/Permission NativeCamera::CheckPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_CheckPermission_m0445839C87DFBA62F088F4A2116FFA53173470CD (bool ___isPicturePermission0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral660F6D5965E09393894520A3BBDDAE9F5DEF81D2);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// Permission result = (Permission) AJC.CallStatic<int>( "CheckPermission", Context, isPicturePermission );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0;
		L_0 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)2);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3;
		L_3 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_2;
		bool L_5 = ___isPicturePermission0;
		bool L_6 = L_5;
		RuntimeObject* L_7 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_6);
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_7);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_7);
		NullCheck(L_0);
		int32_t L_8;
		L_8 = AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A(L_0, _stringLiteral660F6D5965E09393894520A3BBDDAE9F5DEF81D2, L_4, AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A_RuntimeMethod_var);
		V_0 = L_8;
		// if( result == Permission.Denied && (Permission) PlayerPrefs.GetInt( "NativeCameraPermission", (int) Permission.ShouldAsk ) == Permission.ShouldAsk )
		int32_t L_9 = V_0;
		if (L_9)
		{
			goto IL_003a;
		}
	}
	{
		int32_t L_10;
		L_10 = PlayerPrefs_GetInt_m8AD1FA8BA54CC6CE2B2AEEE36B6D75587BB1692D(_stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8, 2, NULL);
		if ((!(((uint32_t)L_10) == ((uint32_t)2))))
		{
			goto IL_003a;
		}
	}
	{
		// result = Permission.ShouldAsk;
		V_0 = 2;
	}

IL_003a:
	{
		// return result;
		int32_t L_11 = V_0;
		return L_11;
	}
}
// NativeCamera/Permission NativeCamera::RequestPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_RequestPermission_m22C7DBA897A6DB77BFB1C2DF886FC1F331CFEDB1 (bool ___isPicturePermission0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0443845674FDE6986E4ECC72A8C096004DF51FC6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	bool V_2 = false;
	NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* V_3 = NULL;
	int32_t V_4 = 0;
	{
		// if( CheckPermission( isPicturePermission ) == Permission.Granted )
		bool L_0 = ___isPicturePermission0;
		int32_t L_1;
		L_1 = NativeCamera_CheckPermission_m0445839C87DFBA62F088F4A2116FFA53173470CD(L_0, NULL);
		if ((!(((uint32_t)L_1) == ((uint32_t)1))))
		{
			goto IL_000b;
		}
	}
	{
		// return Permission.Granted;
		return (int32_t)(1);
	}

IL_000b:
	{
		// object threadLock = new object();
		RuntimeObject* L_2 = (RuntimeObject*)il2cpp_codegen_object_new(RuntimeObject_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(L_2, NULL);
		V_0 = L_2;
		// lock( threadLock )
		RuntimeObject* L_3 = V_0;
		V_1 = L_3;
		V_2 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00a2:
			{// begin finally (depth: 1)
				{
					bool L_4 = V_2;
					if (!L_4)
					{
						goto IL_00ab;
					}
				}
				{
					RuntimeObject* L_5 = V_1;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_5, NULL);
				}

IL_00ab:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				RuntimeObject* L_6 = V_1;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_6, (&V_2), NULL);
				// NCPermissionCallbackAndroid nativeCallback = new NCPermissionCallbackAndroid( threadLock );
				RuntimeObject* L_7 = V_0;
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_8 = (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91*)il2cpp_codegen_object_new(NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91_il2cpp_TypeInfo_var);
				NullCheck(L_8);
				NCPermissionCallbackAndroid__ctor_mEE399CA43702611369BBFD6E72E3147E4C962FDC(L_8, L_7, NULL);
				V_3 = L_8;
				// AJC.CallStatic( "RequestPermission", Context, nativeCallback, isPicturePermission, (int) Permission.ShouldAsk );
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_9;
				L_9 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11 = L_10;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_12;
				L_12 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
				NullCheck(L_11);
				ArrayElementTypeCheck (L_11, L_12);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_12);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13 = L_11;
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_14 = V_3;
				NullCheck(L_13);
				ArrayElementTypeCheck (L_13, L_14);
				(L_13)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_14);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_15 = L_13;
				bool L_16 = ___isPicturePermission0;
				bool L_17 = L_16;
				RuntimeObject* L_18 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_17);
				NullCheck(L_15);
				ArrayElementTypeCheck (L_15, L_18);
				(L_15)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_18);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_19 = L_15;
				int32_t L_20 = 2;
				RuntimeObject* L_21 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_20);
				NullCheck(L_19);
				ArrayElementTypeCheck (L_19, L_21);
				(L_19)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_21);
				NullCheck(L_9);
				AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C(L_9, _stringLiteral0443845674FDE6986E4ECC72A8C096004DF51FC6, L_19, NULL);
				// if( nativeCallback.Result == -1 )
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_22 = V_3;
				NullCheck(L_22);
				int32_t L_23;
				L_23 = NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline(L_22, NULL);
				if ((!(((uint32_t)L_23) == ((uint32_t)(-1)))))
				{
					goto IL_0067_1;
				}
			}
			{
				// System.Threading.Monitor.Wait( threadLock );
				RuntimeObject* L_24 = V_0;
				bool L_25;
				L_25 = Monitor_Wait_m322138959FFE3F4F3212658ACB0C30C981880D28(L_24, NULL);
			}

IL_0067_1:
			{
				// if( (Permission) nativeCallback.Result != Permission.ShouldAsk && PlayerPrefs.GetInt( "NativeCameraPermission", -1 ) != nativeCallback.Result )
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_26 = V_3;
				NullCheck(L_26);
				int32_t L_27;
				L_27 = NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline(L_26, NULL);
				if ((((int32_t)L_27) == ((int32_t)2)))
				{
					goto IL_0098_1;
				}
			}
			{
				int32_t L_28;
				L_28 = PlayerPrefs_GetInt_m8AD1FA8BA54CC6CE2B2AEEE36B6D75587BB1692D(_stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8, (-1), NULL);
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_29 = V_3;
				NullCheck(L_29);
				int32_t L_30;
				L_30 = NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline(L_29, NULL);
				if ((((int32_t)L_28) == ((int32_t)L_30)))
				{
					goto IL_0098_1;
				}
			}
			{
				// PlayerPrefs.SetInt( "NativeCameraPermission", nativeCallback.Result );
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_31 = V_3;
				NullCheck(L_31);
				int32_t L_32;
				L_32 = NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline(L_31, NULL);
				PlayerPrefs_SetInt_m956D3E2DB966F20CF42F842880DDF9E2BE94D948(_stringLiteral17A2974EBDD10F9D99410DD81D1AD62BFB8922B8, L_32, NULL);
				// PlayerPrefs.Save();
				PlayerPrefs_Save_m82567E045D69C838112EA204B60C144D4C1EA3AE(NULL);
			}

IL_0098_1:
			{
				// return (Permission) nativeCallback.Result;
				NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* L_33 = V_3;
				NullCheck(L_33);
				int32_t L_34;
				L_34 = NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline(L_33, NULL);
				V_4 = L_34;
				goto IL_00ac;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00ac:
	{
		// }
		int32_t L_35 = V_4;
		return L_35;
	}
}
// System.Void NativeCamera::RequestPermissionAsync(NativeCamera/PermissionCallback,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeCamera_RequestPermissionAsync_mC8600A67BB4F1B65AB202009AA15E8A1406395F3 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* ___callback0, bool ___isPicturePermission1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0443845674FDE6986E4ECC72A8C096004DF51FC6);
		s_Il2CppMethodInitialized = true;
	}
	NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* V_0 = NULL;
	{
		// NCPermissionCallbackAsyncAndroid nativeCallback = new NCPermissionCallbackAsyncAndroid( callback );
		PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* L_0 = ___callback0;
		NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* L_1 = (NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B*)il2cpp_codegen_object_new(NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		NCPermissionCallbackAsyncAndroid__ctor_m148D3C8505CACDED882A222F2081610060ACF67F(L_1, L_0, NULL);
		V_0 = L_1;
		// AJC.CallStatic( "RequestPermission", Context, nativeCallback, isPicturePermission, (int) Permission.ShouldAsk );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_2;
		L_2 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_3;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5;
		L_5 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = L_4;
		NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* L_7 = V_0;
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_7);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_7);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_8 = L_6;
		bool L_9 = ___isPicturePermission1;
		bool L_10 = L_9;
		RuntimeObject* L_11 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_10);
		NullCheck(L_8);
		ArrayElementTypeCheck (L_8, L_11);
		(L_8)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_11);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_12 = L_8;
		int32_t L_13 = 2;
		RuntimeObject* L_14 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_13);
		NullCheck(L_12);
		ArrayElementTypeCheck (L_12, L_14);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_14);
		NullCheck(L_2);
		AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C(L_2, _stringLiteral0443845674FDE6986E4ECC72A8C096004DF51FC6, L_12, NULL);
		// }
		return;
	}
}
// System.Threading.Tasks.Task`1<NativeCamera/Permission> NativeCamera::RequestPermissionAsync(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED* NativeCamera_RequestPermissionAsync_m6DF2C8CC7726FC872CA507EA8312314CF89A8A40 (bool ___isPicturePermission0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskCompletionSource_1__ctor_m59FEBA96E2C649BB6B9E4A681806C7ED628834FB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskCompletionSource_1_get_Task_mDCD7F80204B3108705062BD8783CD541E08A4157_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass20_0_U3CRequestPermissionAsyncU3Eb__0_m5DF168C3CFBC1336DC14F7F3C6B889DF554A7ED1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* L_0 = (U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass20_0__ctor_mFBDEA24DAAE658B4CB10A30CA2C7BE3D5F8F8655(L_0, NULL);
		// TaskCompletionSource<Permission> tcs = new TaskCompletionSource<Permission>();
		U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* L_1 = L_0;
		TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* L_2 = (TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531*)il2cpp_codegen_object_new(TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		TaskCompletionSource_1__ctor_m59FEBA96E2C649BB6B9E4A681806C7ED628834FB(L_2, TaskCompletionSource_1__ctor_m59FEBA96E2C649BB6B9E4A681806C7ED628834FB_RuntimeMethod_var);
		NullCheck(L_1);
		L_1->___tcs_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___tcs_0), (void*)L_2);
		// RequestPermissionAsync( ( permission ) => tcs.SetResult( permission ), isPicturePermission );
		U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* L_3 = L_1;
		PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* L_4 = (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001*)il2cpp_codegen_object_new(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		PermissionCallback__ctor_m7CA919F20741081C2F81A209707B998CCC4D3782(L_4, L_3, (intptr_t)((void*)U3CU3Ec__DisplayClass20_0_U3CRequestPermissionAsyncU3Eb__0_m5DF168C3CFBC1336DC14F7F3C6B889DF554A7ED1_RuntimeMethod_var), NULL);
		bool L_5 = ___isPicturePermission0;
		NativeCamera_RequestPermissionAsync_mC8600A67BB4F1B65AB202009AA15E8A1406395F3(L_4, L_5, NULL);
		// return tcs.Task;
		NullCheck(L_3);
		TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* L_6 = L_3->___tcs_0;
		NullCheck(L_6);
		Task_1_t75D76B0D2A565B05D89753E60648134269CB6DED* L_7;
		L_7 = TaskCompletionSource_1_get_Task_mDCD7F80204B3108705062BD8783CD541E08A4157_inline(L_6, TaskCompletionSource_1_get_Task_mDCD7F80204B3108705062BD8783CD541E08A4157_RuntimeMethod_var);
		return L_7;
	}
}
// System.Boolean NativeCamera::CanOpenSettings()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeCamera_CanOpenSettings_m56BCC77D77E381E5181E956C018F4BBFB4E102BA (const RuntimeMethod* method) 
{
	{
		// return true;
		return (bool)1;
	}
}
// System.Void NativeCamera::OpenSettings()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeCamera_OpenSettings_m8732D49A39F244910C6722CB78FD44694C0C254B (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8A4D2503591255173AC6769BB8A784B9CC5B5BC6);
		s_Il2CppMethodInitialized = true;
	}
	{
		// AJC.CallStatic( "OpenSettings", Context );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0;
		L_0 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3;
		L_3 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		NullCheck(L_0);
		AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C(L_0, _stringLiteral8A4D2503591255173AC6769BB8A784B9CC5B5BC6, L_2, NULL);
		// }
		return;
	}
}
// NativeCamera/Permission NativeCamera::TakePicture(NativeCamera/CameraCallback,System.Int32,System.Boolean,NativeCamera/PreferredCamera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_TakePicture_mC2A748E023DC5C34D19AF571325AF952A714AAD4 (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* ___callback0, int32_t ___maxSize1, bool ___saveAsJPEG2, int32_t ___preferredCamera3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral811A5CF8DF2E4D4CA546FECCBB9A503DC8D89283);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// Permission result = RequestPermission( true );
		int32_t L_0;
		L_0 = NativeCamera_RequestPermission_m22C7DBA897A6DB77BFB1C2DF886FC1F331CFEDB1((bool)1, NULL);
		V_0 = L_0;
		// if( result == Permission.Granted && !IsCameraBusy() )
		int32_t L_1 = V_0;
		if ((!(((uint32_t)L_1) == ((uint32_t)1))))
		{
			goto IL_0041;
		}
	}
	{
		bool L_2;
		L_2 = NativeCamera_IsCameraBusy_m3373F1A16F70303B7F53D47A1143BED4152C2F54(NULL);
		if (L_2)
		{
			goto IL_0041;
		}
	}
	{
		// AJC.CallStatic( "TakePicture", Context, new NCCameraCallbackAndroid( callback ), (int) preferredCamera );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_3;
		L_3 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)3);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = L_4;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_6;
		L_6 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_5);
		ArrayElementTypeCheck (L_5, L_6);
		(L_5)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7 = L_5;
		CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* L_8 = ___callback0;
		NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* L_9 = (NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378*)il2cpp_codegen_object_new(NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378_il2cpp_TypeInfo_var);
		NullCheck(L_9);
		NCCameraCallbackAndroid__ctor_m7A49A723EBFBFC7215432493733AFCF632A8F5AA(L_9, L_8, NULL);
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, L_9);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_9);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = L_7;
		int32_t L_11 = ___preferredCamera3;
		int32_t L_12 = ((int32_t)L_11);
		RuntimeObject* L_13 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_12);
		NullCheck(L_10);
		ArrayElementTypeCheck (L_10, L_13);
		(L_10)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_13);
		NullCheck(L_3);
		AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C(L_3, _stringLiteral811A5CF8DF2E4D4CA546FECCBB9A503DC8D89283, L_10, NULL);
	}

IL_0041:
	{
		// return result;
		int32_t L_14 = V_0;
		return L_14;
	}
}
// NativeCamera/Permission NativeCamera::RecordVideo(NativeCamera/CameraCallback,NativeCamera/Quality,System.Int32,System.Int64,NativeCamera/PreferredCamera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeCamera_RecordVideo_mFC9802F5F9A52504C23E339018820D8CED325292 (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* ___callback0, int32_t ___quality1, int32_t ___maxDuration2, int64_t ___maxSizeBytes3, int32_t ___preferredCamera4, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFFB00AB07E4FD5417BED928D76EA5DBF492DFDAE);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// Permission result = RequestPermission( false );
		int32_t L_0;
		L_0 = NativeCamera_RequestPermission_m22C7DBA897A6DB77BFB1C2DF886FC1F331CFEDB1((bool)0, NULL);
		V_0 = L_0;
		// if( result == Permission.Granted && !IsCameraBusy() )
		int32_t L_1 = V_0;
		if ((!(((uint32_t)L_1) == ((uint32_t)1))))
		{
			goto IL_005d;
		}
	}
	{
		bool L_2;
		L_2 = NativeCamera_IsCameraBusy_m3373F1A16F70303B7F53D47A1143BED4152C2F54(NULL);
		if (L_2)
		{
			goto IL_005d;
		}
	}
	{
		// AJC.CallStatic( "RecordVideo", Context, new NCCameraCallbackAndroid( callback ), (int) preferredCamera, (int) quality, maxDuration, maxSizeBytes );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_3;
		L_3 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = L_4;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_6;
		L_6 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_5);
		ArrayElementTypeCheck (L_5, L_6);
		(L_5)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7 = L_5;
		CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* L_8 = ___callback0;
		NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* L_9 = (NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378*)il2cpp_codegen_object_new(NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378_il2cpp_TypeInfo_var);
		NullCheck(L_9);
		NCCameraCallbackAndroid__ctor_m7A49A723EBFBFC7215432493733AFCF632A8F5AA(L_9, L_8, NULL);
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, L_9);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_9);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = L_7;
		int32_t L_11 = ___preferredCamera4;
		int32_t L_12 = ((int32_t)L_11);
		RuntimeObject* L_13 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_12);
		NullCheck(L_10);
		ArrayElementTypeCheck (L_10, L_13);
		(L_10)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_13);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = L_10;
		int32_t L_15 = ___quality1;
		int32_t L_16 = ((int32_t)L_15);
		RuntimeObject* L_17 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_16);
		NullCheck(L_14);
		ArrayElementTypeCheck (L_14, L_17);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_17);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_18 = L_14;
		int32_t L_19 = ___maxDuration2;
		int32_t L_20 = L_19;
		RuntimeObject* L_21 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_20);
		NullCheck(L_18);
		ArrayElementTypeCheck (L_18, L_21);
		(L_18)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_21);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_22 = L_18;
		int64_t L_23 = ___maxSizeBytes3;
		int64_t L_24 = L_23;
		RuntimeObject* L_25 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_24);
		NullCheck(L_22);
		ArrayElementTypeCheck (L_22, L_25);
		(L_22)->SetAt(static_cast<il2cpp_array_size_t>(5), (RuntimeObject*)L_25);
		NullCheck(L_3);
		AndroidJavaObject_CallStatic_mB677DE04369EDD8E6DECAF2F233116EE1F06555C(L_3, _stringLiteralFFB00AB07E4FD5417BED928D76EA5DBF492DFDAE, L_22, NULL);
	}

IL_005d:
	{
		// return result;
		int32_t L_26 = V_0;
		return L_26;
	}
}
// System.Boolean NativeCamera::DeviceHasCamera()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeCamera_DeviceHasCamera_m73F0E99DB7C414EA95D5600918FFC502373E41E7 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBB89381AF6FA67A07F15BA3C2582693F59E071ED);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return AJC.CallStatic<bool>( "HasCamera", Context );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0;
		L_0 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3;
		L_3 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		NullCheck(L_0);
		bool L_4;
		L_4 = AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6(L_0, _stringLiteralBB89381AF6FA67A07F15BA3C2582693F59E071ED, L_2, AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6_RuntimeMethod_var);
		return L_4;
	}
}
// System.Boolean NativeCamera::IsCameraBusy()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeCamera_IsCameraBusy_m3373F1A16F70303B7F53D47A1143BED4152C2F54 (const RuntimeMethod* method) 
{
	{
		// return false;
		return (bool)0;
	}
}
// UnityEngine.Texture2D NativeCamera::LoadImageAtPath(System.String,System.Int32,System.Boolean,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747 (String_t* ___imagePath0, int32_t ___maxSize1, bool ___markTextureNonReadable2, bool ___generateMipmaps3, bool ___linearColorSpace4, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral76F1B85647641622FD867CE16AF6C584C5081BD4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDD146CE30524569A8784D1FFE34EA505C910727D);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	String_t* V_1 = NULL;
	int32_t V_2 = 0;
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* V_3 = NULL;
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* V_4 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	int32_t G_B10_0 = 0;
	{
		// if( string.IsNullOrEmpty( imagePath ) )
		String_t* L_0 = ___imagePath0;
		bool L_1;
		L_1 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_0, NULL);
		if (!L_1)
		{
			goto IL_0013;
		}
	}
	{
		// throw new ArgumentException( "Parameter 'imagePath' is null or empty!" );
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_2 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral18B82B6B7DC4FE1988BA61A3784D1768F6C925DF)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747_RuntimeMethod_var)));
	}

IL_0013:
	{
		// if( !File.Exists( imagePath ) )
		String_t* L_3 = ___imagePath0;
		bool L_4;
		L_4 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_3, NULL);
		if (L_4)
		{
			goto IL_002c;
		}
	}
	{
		// throw new FileNotFoundException( "File not found at " + imagePath );
		String_t* L_5 = ___imagePath0;
		String_t* L_6;
		L_6 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral491B4D9271839F0BD63211437BF7CEE5B2C6ADE9)), L_5, NULL);
		FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A* L_7 = (FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A_il2cpp_TypeInfo_var)));
		NullCheck(L_7);
		FileNotFoundException__ctor_mA8C9C93DB8C5B96D6B5E59B2AE07154F265FB1A1(L_7, L_6, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747_RuntimeMethod_var)));
	}

IL_002c:
	{
		// if( maxSize <= 0 )
		int32_t L_8 = ___maxSize1;
		if ((((int32_t)L_8) > ((int32_t)0)))
		{
			goto IL_0037;
		}
	}
	{
		// maxSize = SystemInfo.maxTextureSize;
		int32_t L_9;
		L_9 = SystemInfo_get_maxTextureSize_mEE557C09643222591C6F4D3F561D7A60CD403991(NULL);
		___maxSize1 = L_9;
	}

IL_0037:
	{
		// string loadPath = AJC.CallStatic<string>( "LoadImageAtPath", Context, imagePath, TemporaryImagePath, maxSize );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_10;
		L_10 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_12 = L_11;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_13;
		L_13 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_12);
		ArrayElementTypeCheck (L_12, L_13);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_13);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = L_12;
		String_t* L_15 = ___imagePath0;
		NullCheck(L_14);
		ArrayElementTypeCheck (L_14, L_15);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_15);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_16 = L_14;
		String_t* L_17;
		L_17 = NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027(NULL);
		NullCheck(L_16);
		ArrayElementTypeCheck (L_16, L_17);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_17);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_18 = L_16;
		int32_t L_19 = ___maxSize1;
		int32_t L_20 = L_19;
		RuntimeObject* L_21 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_20);
		NullCheck(L_18);
		ArrayElementTypeCheck (L_18, L_21);
		(L_18)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_21);
		NullCheck(L_10);
		String_t* L_22;
		L_22 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_10, _stringLiteralDD146CE30524569A8784D1FFE34EA505C910727D, L_18, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		V_0 = L_22;
		// string extension = Path.GetExtension( imagePath ).ToLowerInvariant();
		String_t* L_23 = ___imagePath0;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_24;
		L_24 = Path_GetExtension_m6FEAA9E14451BFD210B9D1AEC2430C813F570FE5(L_23, NULL);
		NullCheck(L_24);
		String_t* L_25;
		L_25 = String_ToLowerInvariant_mBE32C93DE27C5353FEA3FA654FC1DDBE3D0EB0F2(L_24, NULL);
		V_1 = L_25;
		// TextureFormat format = ( extension == ".jpg" || extension == ".jpeg" ) ? TextureFormat.RGB24 : TextureFormat.RGBA32;
		String_t* L_26 = V_1;
		bool L_27;
		L_27 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_26, _stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A, NULL);
		if (L_27)
		{
			goto IL_0093;
		}
	}
	{
		String_t* L_28 = V_1;
		bool L_29;
		L_29 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_28, _stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42, NULL);
		if (L_29)
		{
			goto IL_0093;
		}
	}
	{
		G_B10_0 = 4;
		goto IL_0094;
	}

IL_0093:
	{
		G_B10_0 = 3;
	}

IL_0094:
	{
		V_2 = G_B10_0;
		// Texture2D result = new Texture2D( 2, 2, format, generateMipmaps, linearColorSpace );
		int32_t L_30 = V_2;
		bool L_31 = ___generateMipmaps3;
		bool L_32 = ___linearColorSpace4;
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_33 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)il2cpp_codegen_object_new(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		NullCheck(L_33);
		Texture2D__ctor_mC3F84195D1DCEFC0536B3FBD40A8F8E865A4F32A(L_33, 2, 2, L_30, L_31, L_32, NULL);
		V_3 = L_33;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00dd:
			{// begin finally (depth: 1)
				{
					// if( loadPath != imagePath )
					String_t* L_34 = V_0;
					String_t* L_35 = ___imagePath0;
					bool L_36;
					L_36 = String_op_Inequality_m8C940F3CFC42866709D7CA931B3D77B4BE94BCB6(L_34, L_35, NULL);
					if (!L_36)
					{
						goto IL_00f1;
					}
				}
				try
				{// begin try (depth: 2)
					// File.Delete( loadPath );
					String_t* L_37 = V_0;
					File_Delete_mE29829DA504F3E1B8BCB78F21E2862C9ED7EC386(L_37, NULL);
					// }
					goto IL_00f1;
				}// end try (depth: 2)
				catch(Il2CppExceptionWrapper& e)
				{
					if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
					{
						IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
						goto CATCH_00ee;
					}
					throw e;
				}

CATCH_00ee:
				{// begin catch(System.Object)
					// catch { }
					// catch { }
					IL2CPP_POP_ACTIVE_EXCEPTION();
					goto IL_00f1;
				}// end catch (depth: 2)

IL_00f1:
				{
					// }
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			try
			{// begin try (depth: 2)
				{
					// if( !result.LoadImage( File.ReadAllBytes( loadPath ), markTextureNonReadable ) )
					Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_38 = V_3;
					String_t* L_39 = V_0;
					ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_40;
					L_40 = File_ReadAllBytes_m704CBBA3F130C94F5A3E0BE2A93D9E9D79DC3E24(L_39, NULL);
					bool L_41 = ___markTextureNonReadable2;
					bool L_42;
					L_42 = ImageConversion_LoadImage_m292ADCEED268A0A0AAD532BAB8D1710CF0FC8AEF(L_38, L_40, L_41, NULL);
					if (L_42)
					{
						goto IL_00cb_2;
					}
				}
				{
					// Debug.LogWarning( "Couldn't load image at path: " + loadPath );
					String_t* L_43 = V_0;
					String_t* L_44;
					L_44 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral76F1B85647641622FD867CE16AF6C584C5081BD4, L_43, NULL);
					il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
					Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_44, NULL);
					// Object.DestroyImmediate( result );
					Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_45 = V_3;
					il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
					Object_DestroyImmediate_m6336EBC83591A5DB64EC70C92132824C6E258705(L_45, NULL);
					// return null;
					V_4 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
					goto IL_00f4;
				}

IL_00cb_2:
				{
					// }
					goto IL_00f2;
				}
			}// end try (depth: 2)
			catch(Il2CppExceptionWrapper& e)
			{
				if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
				{
					IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
					goto CATCH_00cd_1;
				}
				throw e;
			}

CATCH_00cd_1:
			{// begin catch(System.Exception)
				// Debug.LogException( e );
				il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var)));
				Debug_LogException_mAB3F4DC7297ED8FBB49DAA718B70E59A6B0171B0(((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*)), NULL);
				// Object.DestroyImmediate( result );
				Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_46 = V_3;
				il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var)));
				Object_DestroyImmediate_m6336EBC83591A5DB64EC70C92132824C6E258705(L_46, NULL);
				// return null;
				V_4 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
				IL2CPP_POP_ACTIVE_EXCEPTION();
				goto IL_00f4;
			}// end catch (depth: 2)
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f2:
	{
		// return result;
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_47 = V_3;
		return L_47;
	}

IL_00f4:
	{
		// }
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_48 = V_4;
		return L_48;
	}
}
// System.Threading.Tasks.Task`1<UnityEngine.Texture2D> NativeCamera::LoadImageAtPathAsync(System.String,System.Int32,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t95921EB64E237ACD28589D64B693C652268F225E* NativeCamera_LoadImageAtPathAsync_m7B912F60F0DF9C36558976E4F53701CF76073FC0 (String_t* ___imagePath0, int32_t ___maxSize1, bool ___markTextureNonReadable2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mC08662C58644EB7E11271EF5EDFB23D1678CB978_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_runtime_class_init_inline(AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_il2cpp_TypeInfo_var);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F L_0;
		L_0 = AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4(AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4_RuntimeMethod_var);
		(&V_0)->___U3CU3Et__builder_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)&((&(((&(&V_0)->___U3CU3Et__builder_1))->___m_coreState_1))->___m_stateMachine_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&(&V_0)->___U3CU3Et__builder_1))->___m_coreState_1))->___m_defaultContextAction_1), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&(&V_0)->___U3CU3Et__builder_1))->___m_task_2), (void*)NULL);
		#endif
		String_t* L_1 = ___imagePath0;
		(&V_0)->___imagePath_2 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___imagePath_2), (void*)L_1);
		int32_t L_2 = ___maxSize1;
		(&V_0)->___maxSize_3 = L_2;
		bool L_3 = ___markTextureNonReadable2;
		(&V_0)->___markTextureNonReadable_4 = L_3;
		(&V_0)->___U3CU3E1__state_0 = (-1);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_4 = (&(&V_0)->___U3CU3Et__builder_1);
		AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mC08662C58644EB7E11271EF5EDFB23D1678CB978(L_4, (&V_0), AsyncTaskMethodBuilder_1_Start_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mC08662C58644EB7E11271EF5EDFB23D1678CB978_RuntimeMethod_var);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_5 = (&(&V_0)->___U3CU3Et__builder_1);
		Task_1_t95921EB64E237ACD28589D64B693C652268F225E* L_6;
		L_6 = AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E(L_5, AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E_RuntimeMethod_var);
		return L_6;
	}
}
// UnityEngine.Texture2D NativeCamera::GetVideoThumbnail(System.String,System.Int32,System.Double,System.Boolean,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* NativeCamera_GetVideoThumbnail_m7568A298D0A3BEABBDD10C973A4F9EB657C8644B (String_t* ___videoPath0, int32_t ___maxSize1, double ___captureTimeInSeconds2, bool ___markTextureNonReadable3, bool ___generateMipmaps4, bool ___linearColorSpace5, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5D67C2D23D0E67BA40CD70B037A9F218807BB46F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	{
		// if( maxSize <= 0 )
		int32_t L_0 = ___maxSize1;
		if ((((int32_t)L_0) > ((int32_t)0)))
		{
			goto IL_000b;
		}
	}
	{
		// maxSize = SystemInfo.maxTextureSize;
		int32_t L_1;
		L_1 = SystemInfo_get_maxTextureSize_mEE557C09643222591C6F4D3F561D7A60CD403991(NULL);
		___maxSize1 = L_1;
	}

IL_000b:
	{
		// string thumbnailPath = AJC.CallStatic<string>( "GetVideoThumbnail", Context, videoPath, TemporaryImagePath + ".png", false, maxSize, captureTimeInSeconds );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_2;
		L_2 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_3;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5;
		L_5 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = L_4;
		String_t* L_7 = ___videoPath0;
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_7);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_7);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_8 = L_6;
		String_t* L_9;
		L_9 = NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027(NULL);
		String_t* L_10;
		L_10 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(L_9, _stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26, NULL);
		NullCheck(L_8);
		ArrayElementTypeCheck (L_8, L_10);
		(L_8)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_10);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11 = L_8;
		bool L_12 = ((bool)0);
		RuntimeObject* L_13 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_12);
		NullCheck(L_11);
		ArrayElementTypeCheck (L_11, L_13);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_13);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = L_11;
		int32_t L_15 = ___maxSize1;
		int32_t L_16 = L_15;
		RuntimeObject* L_17 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_16);
		NullCheck(L_14);
		ArrayElementTypeCheck (L_14, L_17);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_17);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_18 = L_14;
		double L_19 = ___captureTimeInSeconds2;
		double L_20 = L_19;
		RuntimeObject* L_21 = Box(Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, &L_20);
		NullCheck(L_18);
		ArrayElementTypeCheck (L_18, L_21);
		(L_18)->SetAt(static_cast<il2cpp_array_size_t>(5), (RuntimeObject*)L_21);
		NullCheck(L_2);
		String_t* L_22;
		L_22 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_2, _stringLiteral5D67C2D23D0E67BA40CD70B037A9F218807BB46F, L_18, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		V_0 = L_22;
		// if( !string.IsNullOrEmpty( thumbnailPath ) )
		String_t* L_23 = V_0;
		bool L_24;
		L_24 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_23, NULL);
		if (L_24)
		{
			goto IL_006f;
		}
	}
	{
		// return LoadImageAtPath( thumbnailPath, maxSize, markTextureNonReadable, generateMipmaps, linearColorSpace );
		String_t* L_25 = V_0;
		int32_t L_26 = ___maxSize1;
		bool L_27 = ___markTextureNonReadable3;
		bool L_28 = ___generateMipmaps4;
		bool L_29 = ___linearColorSpace5;
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_30;
		L_30 = NativeCamera_LoadImageAtPath_m0F2B227CF63FE99A67155B8C52A719AF5949C747(L_25, L_26, L_27, L_28, L_29, NULL);
		return L_30;
	}

IL_006f:
	{
		// return null;
		return (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
	}
}
// System.Threading.Tasks.Task`1<UnityEngine.Texture2D> NativeCamera::GetVideoThumbnailAsync(System.String,System.Int32,System.Double,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Task_1_t95921EB64E237ACD28589D64B693C652268F225E* NativeCamera_GetVideoThumbnailAsync_mED350F6F164784DF8E762509F0B22C0BC7EB7A89 (String_t* ___videoPath0, int32_t ___maxSize1, double ___captureTimeInSeconds2, bool ___markTextureNonReadable3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m59F56DB7F0E99D8EA793C64E8735398C0539DBE6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_runtime_class_init_inline(AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F_il2cpp_TypeInfo_var);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F L_0;
		L_0 = AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4(AsyncTaskMethodBuilder_1_Create_mEBDA40894C43C50AA47346AC784F528C9CA1ABD4_RuntimeMethod_var);
		(&V_0)->___U3CU3Et__builder_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)&((&(((&(&V_0)->___U3CU3Et__builder_1))->___m_coreState_1))->___m_stateMachine_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&(&V_0)->___U3CU3Et__builder_1))->___m_coreState_1))->___m_defaultContextAction_1), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&(&V_0)->___U3CU3Et__builder_1))->___m_task_2), (void*)NULL);
		#endif
		String_t* L_1 = ___videoPath0;
		(&V_0)->___videoPath_2 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___videoPath_2), (void*)L_1);
		int32_t L_2 = ___maxSize1;
		(&V_0)->___maxSize_3 = L_2;
		double L_3 = ___captureTimeInSeconds2;
		(&V_0)->___captureTimeInSeconds_4 = L_3;
		bool L_4 = ___markTextureNonReadable3;
		(&V_0)->___markTextureNonReadable_6 = L_4;
		(&V_0)->___U3CU3E1__state_0 = (-1);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_5 = (&(&V_0)->___U3CU3Et__builder_1);
		AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m59F56DB7F0E99D8EA793C64E8735398C0539DBE6(L_5, (&V_0), AsyncTaskMethodBuilder_1_Start_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m59F56DB7F0E99D8EA793C64E8735398C0539DBE6_RuntimeMethod_var);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_6 = (&(&V_0)->___U3CU3Et__builder_1);
		Task_1_t95921EB64E237ACD28589D64B693C652268F225E* L_7;
		L_7 = AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E(L_6, AsyncTaskMethodBuilder_1_get_Task_mC842CA788344F6A0EAB9EFDE97E0FAC79368245E_RuntimeMethod_var);
		return L_7;
	}
}
// NativeCamera/ImageProperties NativeCamera::GetImageProperties(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46 NativeCamera_GetImageProperties_m906832D1F45ECF5821CFA3236192AE053761C050 (String_t* ___imagePath0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0CA4721FC9D82D780671DE2AB61257837402697D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3E96C9BB1B953A85290371E8CE7BB3F3ABB307CC);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral75E05143EB132AAA8A22B48813DB8E6047380821);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral985B72B30ECE05DD4EF5FE142CEE0FB8BF53A98C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA3A21FB44DD18299A19A0B86BA27CEB4EDA6A941);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAFF4AA19F30B5DC5A240F413D92917103536F1AD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCB4507437E3E619ECBAD84410155675EBEB3DB3F);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	String_t* V_3 = NULL;
	int32_t V_4 = 0;
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* V_5 = NULL;
	int32_t V_6 = 0;
	String_t* V_7 = NULL;
	{
		// if( !File.Exists( imagePath ) )
		String_t* L_0 = ___imagePath0;
		bool L_1;
		L_1 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_0, NULL);
		if (L_1)
		{
			goto IL_0019;
		}
	}
	{
		// throw new FileNotFoundException( "File not found at " + imagePath );
		String_t* L_2 = ___imagePath0;
		String_t* L_3;
		L_3 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral491B4D9271839F0BD63211437BF7CEE5B2C6ADE9)), L_2, NULL);
		FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A* L_4 = (FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		FileNotFoundException__ctor_mA8C9C93DB8C5B96D6B5E59B2AE07154F265FB1A1(L_4, L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NativeCamera_GetImageProperties_m906832D1F45ECF5821CFA3236192AE053761C050_RuntimeMethod_var)));
	}

IL_0019:
	{
		// string value = AJC.CallStatic<string>( "GetImageProperties", Context, imagePath );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_5;
		L_5 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)2);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7 = L_6;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_8;
		L_8 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, L_8);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_7;
		String_t* L_10 = ___imagePath0;
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_10);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_10);
		NullCheck(L_5);
		String_t* L_11;
		L_11 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_5, _stringLiteralA3A21FB44DD18299A19A0B86BA27CEB4EDA6A941, L_9, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		V_0 = L_11;
		// int width = 0, height = 0;
		V_1 = 0;
		// int width = 0, height = 0;
		V_2 = 0;
		// string mimeType = null;
		V_3 = (String_t*)NULL;
		// ImageOrientation orientation = ImageOrientation.Unknown;
		V_4 = (-1);
		// if( !string.IsNullOrEmpty( value ) )
		String_t* L_12 = V_0;
		bool L_13;
		L_13 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_12, NULL);
		if (L_13)
		{
			goto IL_0130;
		}
	}
	{
		// string[] properties = value.Split( '>' );
		String_t* L_14 = V_0;
		NullCheck(L_14);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15;
		L_15 = String_Split_m9530B73D02054692283BF35C3A27C8F2230946F4(L_14, ((int32_t)62), 0, NULL);
		V_5 = L_15;
		// if( properties != null && properties.Length >= 4 )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = V_5;
		if (!L_16)
		{
			goto IL_0130;
		}
	}
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = V_5;
		NullCheck(L_17);
		if ((((int32_t)((int32_t)(((RuntimeArray*)L_17)->max_length))) < ((int32_t)4)))
		{
			goto IL_0130;
		}
	}
	{
		// if( !int.TryParse( properties[0].Trim(), out width ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_18 = V_5;
		NullCheck(L_18);
		int32_t L_19 = 0;
		String_t* L_20 = (L_18)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_20);
		String_t* L_21;
		L_21 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_20, NULL);
		bool L_22;
		L_22 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_21, (&V_1), NULL);
		if (L_22)
		{
			goto IL_007f;
		}
	}
	{
		// width = 0;
		V_1 = 0;
	}

IL_007f:
	{
		// if( !int.TryParse( properties[1].Trim(), out height ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_23 = V_5;
		NullCheck(L_23);
		int32_t L_24 = 1;
		String_t* L_25 = (L_23)->GetAt(static_cast<il2cpp_array_size_t>(L_24));
		NullCheck(L_25);
		String_t* L_26;
		L_26 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_25, NULL);
		bool L_27;
		L_27 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_26, (&V_2), NULL);
		if (L_27)
		{
			goto IL_0093;
		}
	}
	{
		// height = 0;
		V_2 = 0;
	}

IL_0093:
	{
		// mimeType = properties[2].Trim();
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_28 = V_5;
		NullCheck(L_28);
		int32_t L_29 = 2;
		String_t* L_30 = (L_28)->GetAt(static_cast<il2cpp_array_size_t>(L_29));
		NullCheck(L_30);
		String_t* L_31;
		L_31 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_30, NULL);
		V_3 = L_31;
		// if( mimeType.Length == 0 )
		String_t* L_32 = V_3;
		NullCheck(L_32);
		int32_t L_33;
		L_33 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_32, NULL);
		if (L_33)
		{
			goto IL_011a;
		}
	}
	{
		// string extension = Path.GetExtension( imagePath ).ToLowerInvariant();
		String_t* L_34 = ___imagePath0;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_35;
		L_35 = Path_GetExtension_m6FEAA9E14451BFD210B9D1AEC2430C813F570FE5(L_34, NULL);
		NullCheck(L_35);
		String_t* L_36;
		L_36 = String_ToLowerInvariant_mBE32C93DE27C5353FEA3FA654FC1DDBE3D0EB0F2(L_35, NULL);
		V_7 = L_36;
		// if( extension == ".png" )
		String_t* L_37 = V_7;
		bool L_38;
		L_38 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_37, _stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26, NULL);
		if (!L_38)
		{
			goto IL_00c8;
		}
	}
	{
		// mimeType = "image/png";
		V_3 = _stringLiteral75E05143EB132AAA8A22B48813DB8E6047380821;
		goto IL_011a;
	}

IL_00c8:
	{
		// else if( extension == ".jpg" || extension == ".jpeg" )
		String_t* L_39 = V_7;
		bool L_40;
		L_40 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_39, _stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A, NULL);
		if (L_40)
		{
			goto IL_00e4;
		}
	}
	{
		String_t* L_41 = V_7;
		bool L_42;
		L_42 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_41, _stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42, NULL);
		if (!L_42)
		{
			goto IL_00ec;
		}
	}

IL_00e4:
	{
		// mimeType = "image/jpeg";
		V_3 = _stringLiteral3E96C9BB1B953A85290371E8CE7BB3F3ABB307CC;
		goto IL_011a;
	}

IL_00ec:
	{
		// else if( extension == ".gif" )
		String_t* L_43 = V_7;
		bool L_44;
		L_44 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_43, _stringLiteral0CA4721FC9D82D780671DE2AB61257837402697D, NULL);
		if (!L_44)
		{
			goto IL_0102;
		}
	}
	{
		// mimeType = "image/gif";
		V_3 = _stringLiteralAFF4AA19F30B5DC5A240F413D92917103536F1AD;
		goto IL_011a;
	}

IL_0102:
	{
		// else if( extension == ".bmp" )
		String_t* L_45 = V_7;
		bool L_46;
		L_46 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_45, _stringLiteral985B72B30ECE05DD4EF5FE142CEE0FB8BF53A98C, NULL);
		if (!L_46)
		{
			goto IL_0118;
		}
	}
	{
		// mimeType = "image/bmp";
		V_3 = _stringLiteralCB4507437E3E619ECBAD84410155675EBEB3DB3F;
		goto IL_011a;
	}

IL_0118:
	{
		// mimeType = null;
		V_3 = (String_t*)NULL;
	}

IL_011a:
	{
		// if( int.TryParse( properties[3].Trim(), out orientationInt ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_47 = V_5;
		NullCheck(L_47);
		int32_t L_48 = 3;
		String_t* L_49 = (L_47)->GetAt(static_cast<il2cpp_array_size_t>(L_48));
		NullCheck(L_49);
		String_t* L_50;
		L_50 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_49, NULL);
		bool L_51;
		L_51 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_50, (&V_6), NULL);
		if (!L_51)
		{
			goto IL_0130;
		}
	}
	{
		// orientation = (ImageOrientation) orientationInt;
		int32_t L_52 = V_6;
		V_4 = L_52;
	}

IL_0130:
	{
		// return new ImageProperties( width, height, mimeType, orientation );
		int32_t L_53 = V_1;
		int32_t L_54 = V_2;
		String_t* L_55 = V_3;
		int32_t L_56 = V_4;
		ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46 L_57;
		memset((&L_57), 0, sizeof(L_57));
		ImageProperties__ctor_mDB1459622F0FFEEA741B63898D7CE5B41AA4F9EC((&L_57), L_53, L_54, L_55, L_56, /*hidden argument*/NULL);
		return L_57;
	}
}
// NativeCamera/VideoProperties NativeCamera::GetVideoProperties(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E NativeCamera_GetVideoProperties_m2E7F06E4A9C55A06A5752F8337951217018250EB (String_t* ___videoPath0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF811A8F3778A439E75478C3728BE25A7853EAF83);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	int64_t V_3 = 0;
	float V_4 = 0.0f;
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* V_5 = NULL;
	{
		// if( !File.Exists( videoPath ) )
		String_t* L_0 = ___videoPath0;
		bool L_1;
		L_1 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_0, NULL);
		if (L_1)
		{
			goto IL_0019;
		}
	}
	{
		// throw new FileNotFoundException( "File not found at " + videoPath );
		String_t* L_2 = ___videoPath0;
		String_t* L_3;
		L_3 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral491B4D9271839F0BD63211437BF7CEE5B2C6ADE9)), L_2, NULL);
		FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A* L_4 = (FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		FileNotFoundException__ctor_mA8C9C93DB8C5B96D6B5E59B2AE07154F265FB1A1(L_4, L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NativeCamera_GetVideoProperties_m2E7F06E4A9C55A06A5752F8337951217018250EB_RuntimeMethod_var)));
	}

IL_0019:
	{
		// string value = AJC.CallStatic<string>( "GetVideoProperties", Context, videoPath );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_5;
		L_5 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)2);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7 = L_6;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_8;
		L_8 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, L_8);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_7;
		String_t* L_10 = ___videoPath0;
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_10);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_10);
		NullCheck(L_5);
		String_t* L_11;
		L_11 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_5, _stringLiteralF811A8F3778A439E75478C3728BE25A7853EAF83, L_9, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		V_0 = L_11;
		// int width = 0, height = 0;
		V_1 = 0;
		// int width = 0, height = 0;
		V_2 = 0;
		// long duration = 0L;
		V_3 = ((int64_t)0);
		// float rotation = 0f;
		V_4 = (0.0f);
		// if( !string.IsNullOrEmpty( value ) )
		String_t* L_12 = V_0;
		bool L_13;
		L_13 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_12, NULL);
		if (L_13)
		{
			goto IL_00d0;
		}
	}
	{
		// string[] properties = value.Split( '>' );
		String_t* L_14 = V_0;
		NullCheck(L_14);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15;
		L_15 = String_Split_m9530B73D02054692283BF35C3A27C8F2230946F4(L_14, ((int32_t)62), 0, NULL);
		V_5 = L_15;
		// if( properties != null && properties.Length >= 4 )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = V_5;
		if (!L_16)
		{
			goto IL_00d0;
		}
	}
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = V_5;
		NullCheck(L_17);
		if ((((int32_t)((int32_t)(((RuntimeArray*)L_17)->max_length))) < ((int32_t)4)))
		{
			goto IL_00d0;
		}
	}
	{
		// if( !int.TryParse( properties[0].Trim(), out width ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_18 = V_5;
		NullCheck(L_18);
		int32_t L_19 = 0;
		String_t* L_20 = (L_18)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_20);
		String_t* L_21;
		L_21 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_20, NULL);
		bool L_22;
		L_22 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_21, (&V_1), NULL);
		if (L_22)
		{
			goto IL_007b;
		}
	}
	{
		// width = 0;
		V_1 = 0;
	}

IL_007b:
	{
		// if( !int.TryParse( properties[1].Trim(), out height ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_23 = V_5;
		NullCheck(L_23);
		int32_t L_24 = 1;
		String_t* L_25 = (L_23)->GetAt(static_cast<il2cpp_array_size_t>(L_24));
		NullCheck(L_25);
		String_t* L_26;
		L_26 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_25, NULL);
		bool L_27;
		L_27 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_26, (&V_2), NULL);
		if (L_27)
		{
			goto IL_008f;
		}
	}
	{
		// height = 0;
		V_2 = 0;
	}

IL_008f:
	{
		// if( !long.TryParse( properties[2].Trim(), out duration ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_28 = V_5;
		NullCheck(L_28);
		int32_t L_29 = 2;
		String_t* L_30 = (L_28)->GetAt(static_cast<il2cpp_array_size_t>(L_29));
		NullCheck(L_30);
		String_t* L_31;
		L_31 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_30, NULL);
		bool L_32;
		L_32 = Int64_TryParse_m3FC0128C89CC2331239FC2A0A749BF33455F03D2(L_31, (&V_3), NULL);
		if (L_32)
		{
			goto IL_00a4;
		}
	}
	{
		// duration = 0L;
		V_3 = ((int64_t)0);
	}

IL_00a4:
	{
		// if( !float.TryParse( properties[3].Trim().Replace( ',', '.' ), NumberStyles.Float, CultureInfo.InvariantCulture, out rotation ) )
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_33 = V_5;
		NullCheck(L_33);
		int32_t L_34 = 3;
		String_t* L_35 = (L_33)->GetAt(static_cast<il2cpp_array_size_t>(L_34));
		NullCheck(L_35);
		String_t* L_36;
		L_36 = String_Trim_mCD6D8C6D4CFD15225D12DB7D3E0544CA80FB8DA5(L_35, NULL);
		NullCheck(L_36);
		String_t* L_37;
		L_37 = String_Replace_m86403DC5F422D8D5E1CFAAF255B103CB807EDAAF(L_36, ((int32_t)44), ((int32_t)46), NULL);
		il2cpp_codegen_runtime_class_init_inline(CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_il2cpp_TypeInfo_var);
		CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* L_38;
		L_38 = CultureInfo_get_InvariantCulture_mD1E96DC845E34B10F78CB744B0CB5D7D63CEB1E6(NULL);
		bool L_39;
		L_39 = Single_TryParse_mFB8CC32F0016FBB6EFCB97953CF3515767EB6431(L_37, ((int32_t)167), L_38, (&V_4), NULL);
		if (L_39)
		{
			goto IL_00d0;
		}
	}
	{
		// rotation = 0f;
		V_4 = (0.0f);
	}

IL_00d0:
	{
		// if( rotation == -90f )
		float L_40 = V_4;
		if ((!(((float)L_40) == ((float)(-90.0f)))))
		{
			goto IL_00e0;
		}
	}
	{
		// rotation = 270f;
		V_4 = (270.0f);
	}

IL_00e0:
	{
		// return new VideoProperties( width, height, duration, rotation );
		int32_t L_41 = V_1;
		int32_t L_42 = V_2;
		int64_t L_43 = V_3;
		float L_44 = V_4;
		VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E L_45;
		memset((&L_45), 0, sizeof(L_45));
		VideoProperties__ctor_m61A493A9C32C95FE9B3F84709AC0A2E92283F97D((&L_45), L_41, L_42, L_43, L_44, /*hidden argument*/NULL);
		return L_45;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Conversion methods for marshalling of: NativeCamera/ImageProperties
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_pinvoke(const ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46& unmarshaled, ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_pinvoke& marshaled)
{
	marshaled.___width_0 = unmarshaled.___width_0;
	marshaled.___height_1 = unmarshaled.___height_1;
	marshaled.___mimeType_2 = il2cpp_codegen_marshal_string(unmarshaled.___mimeType_2);
	marshaled.___orientation_3 = unmarshaled.___orientation_3;
}
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_pinvoke_back(const ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_pinvoke& marshaled, ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46& unmarshaled)
{
	int32_t unmarshaledwidth_temp_0 = 0;
	unmarshaledwidth_temp_0 = marshaled.___width_0;
	unmarshaled.___width_0 = unmarshaledwidth_temp_0;
	int32_t unmarshaledheight_temp_1 = 0;
	unmarshaledheight_temp_1 = marshaled.___height_1;
	unmarshaled.___height_1 = unmarshaledheight_temp_1;
	unmarshaled.___mimeType_2 = il2cpp_codegen_marshal_string_result(marshaled.___mimeType_2);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___mimeType_2), (void*)il2cpp_codegen_marshal_string_result(marshaled.___mimeType_2));
	int32_t unmarshaledorientation_temp_3 = 0;
	unmarshaledorientation_temp_3 = marshaled.___orientation_3;
	unmarshaled.___orientation_3 = unmarshaledorientation_temp_3;
}
// Conversion method for clean up from marshalling of: NativeCamera/ImageProperties
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_pinvoke_cleanup(ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_marshal_free(marshaled.___mimeType_2);
	marshaled.___mimeType_2 = NULL;
}
// Conversion methods for marshalling of: NativeCamera/ImageProperties
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_com(const ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46& unmarshaled, ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_com& marshaled)
{
	marshaled.___width_0 = unmarshaled.___width_0;
	marshaled.___height_1 = unmarshaled.___height_1;
	marshaled.___mimeType_2 = il2cpp_codegen_marshal_bstring(unmarshaled.___mimeType_2);
	marshaled.___orientation_3 = unmarshaled.___orientation_3;
}
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_com_back(const ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_com& marshaled, ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46& unmarshaled)
{
	int32_t unmarshaledwidth_temp_0 = 0;
	unmarshaledwidth_temp_0 = marshaled.___width_0;
	unmarshaled.___width_0 = unmarshaledwidth_temp_0;
	int32_t unmarshaledheight_temp_1 = 0;
	unmarshaledheight_temp_1 = marshaled.___height_1;
	unmarshaled.___height_1 = unmarshaledheight_temp_1;
	unmarshaled.___mimeType_2 = il2cpp_codegen_marshal_bstring_result(marshaled.___mimeType_2);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___mimeType_2), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___mimeType_2));
	int32_t unmarshaledorientation_temp_3 = 0;
	unmarshaledorientation_temp_3 = marshaled.___orientation_3;
	unmarshaled.___orientation_3 = unmarshaledorientation_temp_3;
}
// Conversion method for clean up from marshalling of: NativeCamera/ImageProperties
IL2CPP_EXTERN_C void ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshal_com_cleanup(ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46_marshaled_com& marshaled)
{
	il2cpp_codegen_marshal_free_bstring(marshaled.___mimeType_2);
	marshaled.___mimeType_2 = NULL;
}
// System.Void NativeCamera/ImageProperties::.ctor(System.Int32,System.Int32,System.String,NativeCamera/ImageOrientation)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ImageProperties__ctor_mDB1459622F0FFEEA741B63898D7CE5B41AA4F9EC (ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46* __this, int32_t ___width0, int32_t ___height1, String_t* ___mimeType2, int32_t ___orientation3, const RuntimeMethod* method) 
{
	{
		// this.width = width;
		int32_t L_0 = ___width0;
		__this->___width_0 = L_0;
		// this.height = height;
		int32_t L_1 = ___height1;
		__this->___height_1 = L_1;
		// this.mimeType = mimeType;
		String_t* L_2 = ___mimeType2;
		__this->___mimeType_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mimeType_2), (void*)L_2);
		// this.orientation = orientation;
		int32_t L_3 = ___orientation3;
		__this->___orientation_3 = L_3;
		// }
		return;
	}
}
IL2CPP_EXTERN_C  void ImageProperties__ctor_mDB1459622F0FFEEA741B63898D7CE5B41AA4F9EC_AdjustorThunk (RuntimeObject* __this, int32_t ___width0, int32_t ___height1, String_t* ___mimeType2, int32_t ___orientation3, const RuntimeMethod* method)
{
	ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ImageProperties_t58867341A9993287DFFDD26325DE0EE68DD52E46*>(__this + _offset);
	ImageProperties__ctor_mDB1459622F0FFEEA741B63898D7CE5B41AA4F9EC(_thisAdjusted, ___width0, ___height1, ___mimeType2, ___orientation3, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/VideoProperties::.ctor(System.Int32,System.Int32,System.Int64,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VideoProperties__ctor_m61A493A9C32C95FE9B3F84709AC0A2E92283F97D (VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E* __this, int32_t ___width0, int32_t ___height1, int64_t ___duration2, float ___rotation3, const RuntimeMethod* method) 
{
	{
		// this.width = width;
		int32_t L_0 = ___width0;
		__this->___width_0 = L_0;
		// this.height = height;
		int32_t L_1 = ___height1;
		__this->___height_1 = L_1;
		// this.duration = duration;
		int64_t L_2 = ___duration2;
		__this->___duration_2 = L_2;
		// this.rotation = rotation;
		float L_3 = ___rotation3;
		__this->___rotation_3 = L_3;
		// }
		return;
	}
}
IL2CPP_EXTERN_C  void VideoProperties__ctor_m61A493A9C32C95FE9B3F84709AC0A2E92283F97D_AdjustorThunk (RuntimeObject* __this, int32_t ___width0, int32_t ___height1, int64_t ___duration2, float ___rotation3, const RuntimeMethod* method)
{
	VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<VideoProperties_t6D7A73E485C96B765AD8710810E46D38907DFC0E*>(__this + _offset);
	VideoProperties__ctor_m61A493A9C32C95FE9B3F84709AC0A2E92283F97D(_thisAdjusted, ___width0, ___height1, ___duration2, ___rotation3, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_Multicast(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* currentDelegate = reinterpret_cast<PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___permission0, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_OpenInst(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___permission0, method);
}
void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_OpenStatic(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___permission0, method);
}
void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_OpenStaticInvoker(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	InvokerActionInvoker1< int32_t >::Invoke(__this->___method_ptr_0, method, NULL, ___permission0);
}
void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_ClosedStaticInvoker(PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	InvokerActionInvoker2< RuntimeObject*, int32_t >::Invoke(__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___permission0);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Native function invocation
	il2cppPInvokeFunc(___permission0);

}
// System.Void NativeCamera/PermissionCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallback__ctor_m7CA919F20741081C2F81A209707B998CCC4D3782 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___method1);
	__this->___method_3 = ___method1;
	__this->___m_target_2 = ___object0;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___object0);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___method1);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___method1))
	{
		bool isOpen = parameterCount == 1;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___method1))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_OpenStatic;
			else
				{
					__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		if (___object0 == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
		__this->___method_code_6 = (intptr_t)__this->___m_target_2;
	}
	__this->___extra_arg_5 = (intptr_t)&PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_Multicast;
}
// System.Void NativeCamera/PermissionCallback::Invoke(NativeCamera/Permission)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___permission0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult NativeCamera/PermissionCallback::BeginInvoke(NativeCamera/Permission,System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PermissionCallback_BeginInvoke_m2623016C072980DFF99367F96400C8B37C88B839 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___callback1, RuntimeObject* ___object2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Permission_t1EEFB992D4BAC04AB077BD49556F33B8B6E0E9F8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	void *__d_args[2] = {0};
	__d_args[0] = Box(Permission_t1EEFB992D4BAC04AB077BD49556F33B8B6E0E9F8_il2cpp_TypeInfo_var, &___permission0);
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___callback1, (RuntimeObject*)___object2);
}
// System.Void NativeCamera/PermissionCallback::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallback_EndInvoke_m2B8461AD4BEF74F43786926160BA150D32090E78 (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, RuntimeObject* ___result0, const RuntimeMethod* method) 
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___result0, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_Multicast(CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* currentDelegate = reinterpret_cast<CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___path0, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenInst(CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	NullCheck(___path0);
	typedef void (*FunctionPointerType) (String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___path0, method);
}
void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenStatic(CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___path0, method);
}
void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenStaticInvoker(CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	InvokerActionInvoker1< String_t* >::Invoke(__this->___method_ptr_0, method, NULL, ___path0);
}
void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_ClosedStaticInvoker(CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	InvokerActionInvoker2< RuntimeObject*, String_t* >::Invoke(__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___path0);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771 (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(char*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Marshaling of parameter '___path0' to native representation
	char* ____path0_marshaled = NULL;
	____path0_marshaled = il2cpp_codegen_marshal_string(___path0);

	// Native function invocation
	il2cppPInvokeFunc(____path0_marshaled);

	// Marshaling cleanup of parameter '___path0' native representation
	il2cpp_codegen_marshal_free(____path0_marshaled);
	____path0_marshaled = NULL;

}
// System.Void NativeCamera/CameraCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraCallback__ctor_m39376148047C9B1A537BC5DB202E142431A77AFE (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___method1);
	__this->___method_3 = ___method1;
	__this->___m_target_2 = ___object0;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___object0);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___method1);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___method1))
	{
		bool isOpen = parameterCount == 1;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___method1))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenStatic;
			else
				{
					__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		bool isOpen = parameterCount == 0;
		if (isOpen)
		{
			__this->___invoke_impl_1 = (intptr_t)&CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_OpenInst;
		}
		else
		{
			if (___object0 == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
			__this->___method_code_6 = (intptr_t)__this->___m_target_2;
		}
	}
	__this->___extra_arg_5 = (intptr_t)&CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_Multicast;
}
// System.Void NativeCamera/CameraCallback::Invoke(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___path0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult NativeCamera/CameraCallback::BeginInvoke(System.String,System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CameraCallback_BeginInvoke_m690E44108A53130266E7C1A57DC6E62F2B0BDCDF (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___callback1, RuntimeObject* ___object2, const RuntimeMethod* method) 
{
	void *__d_args[2] = {0};
	__d_args[0] = ___path0;
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___callback1, (RuntimeObject*)___object2);
}
// System.Void NativeCamera/CameraCallback::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraCallback_EndInvoke_m64D370EA45BBA56CF1B29C19E1B766B43542D7A6 (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, RuntimeObject* ___result0, const RuntimeMethod* method) 
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___result0, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/<>c__DisplayClass20_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass20_0__ctor_mFBDEA24DAAE658B4CB10A30CA2C7BE3D5F8F8655 (U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void NativeCamera/<>c__DisplayClass20_0::<RequestPermissionAsync>b__0(NativeCamera/Permission)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass20_0_U3CRequestPermissionAsyncU3Eb__0_m5DF168C3CFBC1336DC14F7F3C6B889DF554A7ED1 (U3CU3Ec__DisplayClass20_0_t503722069E2F619D174A299988A5C1F3B96FF18E* __this, int32_t ___permission0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskCompletionSource_1_SetResult_mE3BD8B51FE6830985D8550B8E7F71C413B087604_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// RequestPermissionAsync( ( permission ) => tcs.SetResult( permission ), isPicturePermission );
		TaskCompletionSource_1_t64795F3ED695AC716712D41FC9F573ADE5F71531* L_0 = __this->___tcs_0;
		int32_t L_1 = ___permission0;
		NullCheck(L_0);
		TaskCompletionSource_1_SetResult_mE3BD8B51FE6830985D8550B8E7F71C413B087604(L_0, L_1, TaskCompletionSource_1_SetResult_mE3BD8B51FE6830985D8550B8E7F71C413B087604_RuntimeMethod_var);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/<>c__DisplayClass28_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass28_0__ctor_mF21CA894004F6525050D93D9D5332A514261D347 (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.String NativeCamera/<>c__DisplayClass28_0::<LoadImageAtPathAsync>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* U3CU3Ec__DisplayClass28_0_U3CLoadImageAtPathAsyncU3Eb__0_m50534284F00FF8C07BABF2F72B8E9EA780CBF16E (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDD146CE30524569A8784D1FFE34EA505C910727D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// string loadPath = await TryCallNativeAndroidFunctionOnSeparateThread( () => AJC.CallStatic<string>( "LoadImageAtPath", Context, imagePath, temporaryImagePath, maxSize ) );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0;
		L_0 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3;
		L_3 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_2;
		String_t* L_5 = __this->___imagePath_0;
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = L_4;
		String_t* L_7 = __this->___temporaryImagePath_1;
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_7);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_7);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_8 = L_6;
		int32_t L_9 = __this->___maxSize_2;
		int32_t L_10 = L_9;
		RuntimeObject* L_11 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_10);
		NullCheck(L_8);
		ArrayElementTypeCheck (L_8, L_11);
		(L_8)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_11);
		NullCheck(L_0);
		String_t* L_12;
		L_12 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_0, _stringLiteralDD146CE30524569A8784D1FFE34EA505C910727D, L_8, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		return L_12;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/<LoadImageAtPathAsync>d__28::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B (U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mA065D9BD25C55B7B2630CA549183C4615FF27DFC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m56EC0B7A46A5E522CCEAB37456060C36E26CE731_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass28_0_U3CLoadImageAtPathAsyncU3Eb__0_m50534284F00FF8C07BABF2F72B8E9EA780CBF16E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral218F5A08519088A96BE3C1074984C53EA49F1CCA);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral76F1B85647641622FD867CE16AF6C584C5081BD4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3287997374A5B6321B447152239EBE224279EB6);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* V_1 = NULL;
	String_t* V_2 = NULL;
	TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 V_3;
	memset((&V_3), 0, sizeof(V_3));
	YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A V_4;
	memset((&V_4), 0, sizeof(V_4));
	YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB V_5;
	memset((&V_5), 0, sizeof(V_5));
	String_t* V_6 = NULL;
	int32_t V_7 = 0;
	Exception_t* V_8 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 2> __active_exceptions;
	int32_t G_B34_0 = 0;
	{
		int32_t L_0 = __this->___U3CU3E1__state_0;
		V_0 = L_0;
	}
	try
	{// begin try (depth: 1)
		{
			int32_t L_1 = V_0;
			if (!L_1)
			{
				goto IL_0101_1;
			}
		}
		{
			int32_t L_2 = V_0;
			if ((((int32_t)L_2) == ((int32_t)1)))
			{
				goto IL_0154_1;
			}
		}
		{
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_3 = (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB_il2cpp_TypeInfo_var);
			NullCheck(L_3);
			U3CU3Ec__DisplayClass28_0__ctor_mF21CA894004F6525050D93D9D5332A514261D347(L_3, NULL);
			__this->___U3CU3E8__1_5 = L_3;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)L_3);
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_4 = __this->___U3CU3E8__1_5;
			String_t* L_5 = __this->___imagePath_2;
			NullCheck(L_4);
			L_4->___imagePath_0 = L_5;
			Il2CppCodeGenWriteBarrier((void**)(&L_4->___imagePath_0), (void*)L_5);
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_6 = __this->___U3CU3E8__1_5;
			int32_t L_7 = __this->___maxSize_3;
			NullCheck(L_6);
			L_6->___maxSize_2 = L_7;
			// if( string.IsNullOrEmpty( imagePath ) )
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_8 = __this->___U3CU3E8__1_5;
			NullCheck(L_8);
			String_t* L_9 = L_8->___imagePath_0;
			bool L_10;
			L_10 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_9, NULL);
			if (!L_10)
			{
				goto IL_005e_1;
			}
		}
		{
			// throw new ArgumentException( "Parameter 'imagePath' is null or empty!" );
			ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_11 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
			NullCheck(L_11);
			ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_11, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral18B82B6B7DC4FE1988BA61A3784D1768F6C925DF)), NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_11, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B_RuntimeMethod_var)));
		}

IL_005e_1:
		{
			// if( !File.Exists( imagePath ) )
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_12 = __this->___U3CU3E8__1_5;
			NullCheck(L_12);
			String_t* L_13 = L_12->___imagePath_0;
			bool L_14;
			L_14 = File_Exists_m95E329ABBE3EAD6750FE1989BBA6884457136D4A(L_13, NULL);
			if (L_14)
			{
				goto IL_008b_1;
			}
		}
		{
			// throw new FileNotFoundException( "File not found at " + imagePath );
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_15 = __this->___U3CU3E8__1_5;
			NullCheck(L_15);
			String_t* L_16 = L_15->___imagePath_0;
			String_t* L_17;
			L_17 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral491B4D9271839F0BD63211437BF7CEE5B2C6ADE9)), L_16, NULL);
			FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A* L_18 = (FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FileNotFoundException_t17F1B49AD996E4A60C87C7ADC9D3A25EB5808A9A_il2cpp_TypeInfo_var)));
			NullCheck(L_18);
			FileNotFoundException__ctor_mA8C9C93DB8C5B96D6B5E59B2AE07154F265FB1A1(L_18, L_17, NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_18, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B_RuntimeMethod_var)));
		}

IL_008b_1:
		{
			// if( maxSize <= 0 )
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_19 = __this->___U3CU3E8__1_5;
			NullCheck(L_19);
			int32_t L_20 = L_19->___maxSize_2;
			if ((((int32_t)L_20) > ((int32_t)0)))
			{
				goto IL_00a9_1;
			}
		}
		{
			// maxSize = SystemInfo.maxTextureSize;
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_21 = __this->___U3CU3E8__1_5;
			int32_t L_22;
			L_22 = SystemInfo_get_maxTextureSize_mEE557C09643222591C6F4D3F561D7A60CD403991(NULL);
			NullCheck(L_21);
			L_21->___maxSize_2 = L_22;
		}

IL_00a9_1:
		{
			// string temporaryImagePath = TemporaryImagePath; // Must be accessed from main thread
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_23 = __this->___U3CU3E8__1_5;
			String_t* L_24;
			L_24 = NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027(NULL);
			NullCheck(L_23);
			L_23->___temporaryImagePath_1 = L_24;
			Il2CppCodeGenWriteBarrier((void**)(&L_23->___temporaryImagePath_1), (void*)L_24);
			// string loadPath = await TryCallNativeAndroidFunctionOnSeparateThread( () => AJC.CallStatic<string>( "LoadImageAtPath", Context, imagePath, temporaryImagePath, maxSize ) );
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_25 = __this->___U3CU3E8__1_5;
			Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C* L_26 = (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C*)il2cpp_codegen_object_new(Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C_il2cpp_TypeInfo_var);
			NullCheck(L_26);
			Func_1__ctor_m27A68E928C1D9158EAAD261086B9BC424339327B(L_26, L_25, (intptr_t)((void*)U3CU3Ec__DisplayClass28_0_U3CLoadImageAtPathAsyncU3Eb__0_m50534284F00FF8C07BABF2F72B8E9EA780CBF16E_RuntimeMethod_var), NULL);
			Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* L_27;
			L_27 = NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC(L_26, NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC_RuntimeMethod_var);
			NullCheck(L_27);
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_28;
			L_28 = Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F(L_27, Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F_RuntimeMethod_var);
			V_3 = L_28;
			bool L_29;
			L_29 = TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8((&V_3), TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8_RuntimeMethod_var);
			if (L_29)
			{
				goto IL_011d_1;
			}
		}
		{
			int32_t L_30 = 0;
			V_0 = L_30;
			__this->___U3CU3E1__state_0 = L_30;
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_31 = V_3;
			__this->___U3CU3Eu__1_8 = L_31;
			Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CU3Eu__1_8))->___m_task_0), (void*)NULL);
			AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_32 = (&__this->___U3CU3Et__builder_1);
			AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mA065D9BD25C55B7B2630CA549183C4615FF27DFC(L_32, (&V_3), __this, AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_mA065D9BD25C55B7B2630CA549183C4615FF27DFC_RuntimeMethod_var);
			goto IL_037e;
		}

IL_0101_1:
		{
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_33 = __this->___U3CU3Eu__1_8;
			V_3 = L_33;
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* L_34 = (&__this->___U3CU3Eu__1_8);
			il2cpp_codegen_initobj(L_34, sizeof(TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6));
			int32_t L_35 = (-1);
			V_0 = L_35;
			__this->___U3CU3E1__state_0 = L_35;
		}

IL_011d_1:
		{
			String_t* L_36;
			L_36 = TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9((&V_3), TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9_RuntimeMethod_var);
			V_2 = L_36;
			String_t* L_37 = V_2;
			__this->___U3CloadPathU3E5__2_6 = L_37;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CloadPathU3E5__2_6), (void*)L_37);
			// Texture2D result = null;
			__this->___U3CresultU3E5__3_7 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CresultU3E5__3_7), (void*)(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL);
			// using( UnityWebRequest www = UnityWebRequestTexture.GetTexture( "file://" + loadPath, markTextureNonReadable ) )
			String_t* L_38 = __this->___U3CloadPathU3E5__2_6;
			String_t* L_39;
			L_39 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral218F5A08519088A96BE3C1074984C53EA49F1CCA, L_38, NULL);
			bool L_40 = __this->___markTextureNonReadable_4;
			UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_41;
			L_41 = UnityWebRequestTexture_GetTexture_m45F855106C834021AC0DFA25FE31BA14C42693CA(L_39, L_40, NULL);
			__this->___U3CwwwU3E5__4_9 = L_41;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CwwwU3E5__4_9), (void*)L_41);
		}

IL_0154_1:
		{
		}
		{
			auto __finallyBlock = il2cpp::utils::Finally([&]
			{

FINALLY_021e_1:
				{// begin finally (depth: 2)
					{
						int32_t L_42 = V_0;
						if ((((int32_t)L_42) >= ((int32_t)0)))
						{
							goto IL_0235_1;
						}
					}
					{
						UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_43 = __this->___U3CwwwU3E5__4_9;
						if (!L_43)
						{
							goto IL_0235_1;
						}
					}
					{
						UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_44 = __this->___U3CwwwU3E5__4_9;
						NullCheck(L_44);
						InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_44);
					}

IL_0235_1:
					{
						return;
					}
				}// end finally (depth: 2)
			});
			try
			{// begin try (depth: 2)
				{
					int32_t L_45 = V_0;
					if ((((int32_t)L_45) == ((int32_t)1)))
					{
						goto IL_01a9_2;
					}
				}
				{
					// UnityWebRequestAsyncOperation asyncOperation = www.SendWebRequest();
					UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_46 = __this->___U3CwwwU3E5__4_9;
					NullCheck(L_46);
					UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* L_47;
					L_47 = UnityWebRequest_SendWebRequest_mA3CD13983BAA5074A0640EDD661B1E46E6DB6C13(L_46, NULL);
					__this->___U3CasyncOperationU3E5__5_10 = L_47;
					Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CasyncOperationU3E5__5_10), (void*)L_47);
					goto IL_01cd_2;
				}

IL_016c_2:
				{
					// await Task.Yield();
					il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
					YieldAwaitable_tFEA898DB9022A953958C3CF531E1477D135D3DAB L_48;
					L_48 = Task_Yield_m27EE257EF53788244C5B2E874C514C24C693F9B1(NULL);
					V_5 = L_48;
					YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A L_49;
					L_49 = YieldAwaitable_GetAwaiter_m359A05B8C1B9F3F1E9CAE29AD231C0987718DE5E((&V_5), NULL);
					V_4 = L_49;
					bool L_50;
					L_50 = YieldAwaiter_get_IsCompleted_m783B6E67654FDBF490A65AC59972AF6B985A9286((&V_4), NULL);
					if (L_50)
					{
						goto IL_01c6_2;
					}
				}
				{
					int32_t L_51 = 1;
					V_0 = L_51;
					__this->___U3CU3E1__state_0 = L_51;
					YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A L_52 = V_4;
					__this->___U3CU3Eu__2_11 = L_52;
					AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_53 = (&__this->___U3CU3Et__builder_1);
					AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m56EC0B7A46A5E522CCEAB37456060C36E26CE731(L_53, (&V_4), __this, AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisYieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A_TisU3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9_m56EC0B7A46A5E522CCEAB37456060C36E26CE731_RuntimeMethod_var);
					goto IL_037e;
				}

IL_01a9_2:
				{
					YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A L_54 = __this->___U3CU3Eu__2_11;
					V_4 = L_54;
					YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A* L_55 = (&__this->___U3CU3Eu__2_11);
					il2cpp_codegen_initobj(L_55, sizeof(YieldAwaiter_t5F0A81DC85227C01FFC38D53139B5C19D920B52A));
					int32_t L_56 = (-1);
					V_0 = L_56;
					__this->___U3CU3E1__state_0 = L_56;
				}

IL_01c6_2:
				{
					YieldAwaiter_GetResult_m83C9B35D4BBEB09AC5B560912436454D69794F07((&V_4), NULL);
				}

IL_01cd_2:
				{
					// while( !asyncOperation.isDone )
					UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C* L_57 = __this->___U3CasyncOperationU3E5__5_10;
					NullCheck(L_57);
					bool L_58;
					L_58 = AsyncOperation_get_isDone_m68A0682777E2132FC033182E9F50303566AA354D(L_57, NULL);
					if (!L_58)
					{
						goto IL_016c_2;
					}
				}
				{
					// if( www.result != UnityWebRequest.Result.Success )
					UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_59 = __this->___U3CwwwU3E5__4_9;
					NullCheck(L_59);
					int32_t L_60;
					L_60 = UnityWebRequest_get_result_mEF83848C5FCFB5E307CE4B57E42BF02FC9AED449(L_59, NULL);
					if ((((int32_t)L_60) == ((int32_t)1)))
					{
						goto IL_0204_2;
					}
				}
				{
					// Debug.LogWarning( "Couldn't use UnityWebRequest to load image, falling back to LoadImage: " + www.error );
					UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_61 = __this->___U3CwwwU3E5__4_9;
					NullCheck(L_61);
					String_t* L_62;
					L_62 = UnityWebRequest_get_error_m20A5D813ED59118B7AA1D1E2EB5250178B1F5B6F(L_61, NULL);
					String_t* L_63;
					L_63 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3287997374A5B6321B447152239EBE224279EB6, L_62, NULL);
					il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
					Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_63, NULL);
					goto IL_0215_2;
				}

IL_0204_2:
				{
					// result = DownloadHandlerTexture.GetContent( www );
					UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F* L_64 = __this->___U3CwwwU3E5__4_9;
					Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_65;
					L_65 = DownloadHandlerTexture_GetContent_m86BC88F58305A1B21C21CE7D82658197932EFB18(L_64, NULL);
					__this->___U3CresultU3E5__3_7 = L_65;
					Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CresultU3E5__3_7), (void*)L_65);
				}

IL_0215_2:
				{
					// }
					__this->___U3CasyncOperationU3E5__5_10 = (UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C*)NULL;
					Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CasyncOperationU3E5__5_10), (void*)(UnityWebRequestAsyncOperation_t14BE94558FF3A2CFC2EFBE2511A3A88252042B8C*)NULL);
					goto IL_0236_1;
				}
			}// end try (depth: 2)
			catch(Il2CppExceptionWrapper& e)
			{
				__finallyBlock.StoreException(e.ex);
			}
		}

IL_0236_1:
		{
			__this->___U3CwwwU3E5__4_9 = (UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F*)NULL;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CwwwU3E5__4_9), (void*)(UnityWebRequest_t6233B8E22992FC2364A831C1ACB033EF3260C39F*)NULL);
			// if( !result ) // Fallback to Texture2D.LoadImage if something goes wrong
			Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_66 = __this->___U3CresultU3E5__3_7;
			il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
			bool L_67;
			L_67 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_66, NULL);
			if (L_67)
			{
				goto IL_031e_1;
			}
		}
		{
			// string extension = Path.GetExtension( imagePath ).ToLowerInvariant();
			U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_68 = __this->___U3CU3E8__1_5;
			NullCheck(L_68);
			String_t* L_69 = L_68->___imagePath_0;
			il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
			String_t* L_70;
			L_70 = Path_GetExtension_m6FEAA9E14451BFD210B9D1AEC2430C813F570FE5(L_69, NULL);
			NullCheck(L_70);
			String_t* L_71;
			L_71 = String_ToLowerInvariant_mBE32C93DE27C5353FEA3FA654FC1DDBE3D0EB0F2(L_70, NULL);
			V_6 = L_71;
			// TextureFormat format = ( extension == ".jpg" || extension == ".jpeg" ) ? TextureFormat.RGB24 : TextureFormat.RGBA32;
			String_t* L_72 = V_6;
			bool L_73;
			L_73 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_72, _stringLiteral23DF9991B71463C240582D176E347E7E47AEFF5A, NULL);
			if (L_73)
			{
				goto IL_0283_1;
			}
		}
		{
			String_t* L_74 = V_6;
			bool L_75;
			L_75 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_74, _stringLiteral4B9B40AAD718882F5C0B95FE844E4AA92BD49C42, NULL);
			if (L_75)
			{
				goto IL_0283_1;
			}
		}
		{
			G_B34_0 = 4;
			goto IL_0284_1;
		}

IL_0283_1:
		{
			G_B34_0 = 3;
		}

IL_0284_1:
		{
			V_7 = G_B34_0;
			// result = new Texture2D( 2, 2, format, true, false );
			int32_t L_76 = V_7;
			Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_77 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)il2cpp_codegen_object_new(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
			NullCheck(L_77);
			Texture2D__ctor_mC3F84195D1DCEFC0536B3FBD40A8F8E865A4F32A(L_77, 2, 2, L_76, (bool)1, (bool)0, NULL);
			__this->___U3CresultU3E5__3_7 = L_77;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CresultU3E5__3_7), (void*)L_77);
		}
		{
			auto __finallyBlock = il2cpp::utils::Finally([&]
			{

FINALLY_02f1_1:
				{// begin finally (depth: 2)
					{
						int32_t L_78 = V_0;
						if ((((int32_t)L_78) >= ((int32_t)0)))
						{
							goto IL_031d_1;
						}
					}
					{
						// if( loadPath != imagePath )
						String_t* L_79 = __this->___U3CloadPathU3E5__2_6;
						U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB* L_80 = __this->___U3CU3E8__1_5;
						NullCheck(L_80);
						String_t* L_81 = L_80->___imagePath_0;
						bool L_82;
						L_82 = String_op_Inequality_m8C940F3CFC42866709D7CA931B3D77B4BE94BCB6(L_79, L_81, NULL);
						if (!L_82)
						{
							goto IL_031d_1;
						}
					}
					try
					{// begin try (depth: 3)
						// File.Delete( loadPath );
						String_t* L_83 = __this->___U3CloadPathU3E5__2_6;
						File_Delete_mE29829DA504F3E1B8BCB78F21E2862C9ED7EC386(L_83, NULL);
						// }
						goto IL_031d_1;
					}// end try (depth: 3)
					catch(Il2CppExceptionWrapper& e)
					{
						if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
						{
							IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
							goto CATCH_031a_1;
						}
						throw e;
					}

CATCH_031a_1:
					{// begin catch(System.Object)
						// catch { }
						// catch { }
						IL2CPP_POP_ACTIVE_EXCEPTION();
						goto IL_031d_1;
					}// end catch (depth: 3)

IL_031d_1:
					{
						return;
					}
				}// end finally (depth: 2)
			});
			try
			{// begin try (depth: 2)
				try
				{// begin try (depth: 3)
					{
						// if( !result.LoadImage( File.ReadAllBytes( loadPath ), markTextureNonReadable ) )
						Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_84 = __this->___U3CresultU3E5__3_7;
						String_t* L_85 = __this->___U3CloadPathU3E5__2_6;
						ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_86;
						L_86 = File_ReadAllBytes_m704CBBA3F130C94F5A3E0BE2A93D9E9D79DC3E24(L_85, NULL);
						bool L_87 = __this->___markTextureNonReadable_4;
						bool L_88;
						L_88 = ImageConversion_LoadImage_m292ADCEED268A0A0AAD532BAB8D1710CF0FC8AEF(L_84, L_86, L_87, NULL);
						if (L_88)
						{
							goto IL_02d9_3;
						}
					}
					{
						// Debug.LogWarning( "Couldn't load image at path: " + loadPath );
						String_t* L_89 = __this->___U3CloadPathU3E5__2_6;
						String_t* L_90;
						L_90 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral76F1B85647641622FD867CE16AF6C584C5081BD4, L_89, NULL);
						il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
						Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_90, NULL);
						// Object.DestroyImmediate( result );
						Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_91 = __this->___U3CresultU3E5__3_7;
						il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
						Object_DestroyImmediate_m6336EBC83591A5DB64EC70C92132824C6E258705(L_91, NULL);
						// return null;
						V_1 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
						goto IL_0355;
					}

IL_02d9_3:
					{
						// }
						goto IL_02ef_2;
					}
				}// end try (depth: 3)
				catch(Il2CppExceptionWrapper& e)
				{
					if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
					{
						IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
						goto CATCH_02db_2;
					}
					throw e;
				}

CATCH_02db_2:
				{// begin catch(System.Exception)
					// Debug.LogException( e );
					il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var)));
					Debug_LogException_mAB3F4DC7297ED8FBB49DAA718B70E59A6B0171B0(((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*)), NULL);
					// Object.DestroyImmediate( result );
					Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_92 = __this->___U3CresultU3E5__3_7;
					il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var)));
					Object_DestroyImmediate_m6336EBC83591A5DB64EC70C92132824C6E258705(L_92, NULL);
					// return null;
					V_1 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
					IL2CPP_POP_ACTIVE_EXCEPTION();
					goto IL_0355;
				}// end catch (depth: 3)

IL_02ef_2:
				{
					goto IL_031e_1;
				}
			}// end try (depth: 2)
			catch(Il2CppExceptionWrapper& e)
			{
				__finallyBlock.StoreException(e.ex);
			}
		}

IL_031e_1:
		{
			// return result;
			Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_93 = __this->___U3CresultU3E5__3_7;
			V_1 = L_93;
			goto IL_0355;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0327;
		}
		throw e;
	}

CATCH_0327:
	{// begin catch(System.Exception)
		V_8 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));
		__this->___U3CU3E1__state_0 = ((int32_t)-2);
		__this->___U3CU3E8__1_5 = (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)(U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB*)NULL);
		__this->___U3CloadPathU3E5__2_6 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CloadPathU3E5__2_6), (void*)(String_t*)NULL);
		__this->___U3CresultU3E5__3_7 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CresultU3E5__3_7), (void*)(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_94 = (&__this->___U3CU3Et__builder_1);
		Exception_t* L_95 = V_8;
		AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B(L_94, L_95, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B_RuntimeMethod_var)));
		IL2CPP_POP_ACTIVE_EXCEPTION();
		goto IL_037e;
	}// end catch (depth: 1)

IL_0355:
	{
		// }
		__this->___U3CU3E1__state_0 = ((int32_t)-2);
		__this->___U3CU3E8__1_5 = (U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)(U3CU3Ec__DisplayClass28_0_tDD86543B59AF0A67FEC563A522F809F8C9492FCB*)NULL);
		__this->___U3CloadPathU3E5__2_6 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CloadPathU3E5__2_6), (void*)(String_t*)NULL);
		__this->___U3CresultU3E5__3_7 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CresultU3E5__3_7), (void*)(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_96 = (&__this->___U3CU3Et__builder_1);
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_97 = V_1;
		AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688(L_96, L_97, AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688_RuntimeMethod_var);
	}

IL_037e:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9*>(__this + _offset);
	U3CLoadImageAtPathAsyncU3Ed__28_MoveNext_mDDC8FDCD0469319106B32AF4FD60D1EBF7CE804B(_thisAdjusted, method);
}
// System.Void NativeCamera/<LoadImageAtPathAsync>d__28::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadImageAtPathAsyncU3Ed__28_SetStateMachine_mCC03A217715E97CF709DD6F9646EE16946B331B1 (U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_0 = (&__this->___U3CU3Et__builder_1);
		RuntimeObject* L_1 = ___stateMachine0;
		AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD(L_0, L_1, AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD_RuntimeMethod_var);
		return;
	}
}
IL2CPP_EXTERN_C  void U3CLoadImageAtPathAsyncU3Ed__28_SetStateMachine_mCC03A217715E97CF709DD6F9646EE16946B331B1_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method)
{
	U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CLoadImageAtPathAsyncU3Ed__28_t75F66238DD26F927D10159C0541013DEC4979AD9*>(__this + _offset);
	U3CLoadImageAtPathAsyncU3Ed__28_SetStateMachine_mCC03A217715E97CF709DD6F9646EE16946B331B1(_thisAdjusted, ___stateMachine0, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/<>c__DisplayClass30_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass30_0__ctor_m1FE9FD5C2A2C59BAB3B1BD833EE7F70A5D49DACE (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.String NativeCamera/<>c__DisplayClass30_0::<GetVideoThumbnailAsync>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* U3CU3Ec__DisplayClass30_0_U3CGetVideoThumbnailAsyncU3Eb__0_m5DAF2A26314559841D948DC75B130034596DF868 (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5D67C2D23D0E67BA40CD70B037A9F218807BB46F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26);
		s_Il2CppMethodInitialized = true;
	}
	{
		// string thumbnailPath = await TryCallNativeAndroidFunctionOnSeparateThread( () => AJC.CallStatic<string>( "GetVideoThumbnail", Context, videoPath, temporaryImagePath + ".png", false, maxSize, captureTimeInSeconds ) );
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_0;
		L_0 = NativeCamera_get_AJC_m06EF6C7725A276AC3F2639007C713591FE194E5E(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_3;
		L_3 = NativeCamera_get_Context_m4851E6BDFBF25FC16C4B48CE8F966336BE0BADFD(NULL);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = L_2;
		String_t* L_5 = __this->___videoPath_0;
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = L_4;
		String_t* L_7 = __this->___temporaryImagePath_1;
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(L_7, _stringLiteralA15C898F015A9B0BC3268E8883CD03008A56DE26, NULL);
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_8);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_6;
		bool L_10 = ((bool)0);
		RuntimeObject* L_11 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_10);
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_11);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_11);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_12 = L_9;
		int32_t L_13 = __this->___maxSize_2;
		int32_t L_14 = L_13;
		RuntimeObject* L_15 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_14);
		NullCheck(L_12);
		ArrayElementTypeCheck (L_12, L_15);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_15);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_16 = L_12;
		double L_17 = __this->___captureTimeInSeconds_3;
		double L_18 = L_17;
		RuntimeObject* L_19 = Box(Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, &L_18);
		NullCheck(L_16);
		ArrayElementTypeCheck (L_16, L_19);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(5), (RuntimeObject*)L_19);
		NullCheck(L_0);
		String_t* L_20;
		L_20 = AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3(L_0, _stringLiteral5D67C2D23D0E67BA40CD70B037A9F218807BB46F, L_16, AndroidJavaObject_CallStatic_TisString_t_mB5DC41208BD7C326A089C20F4F1C2B8B2444ACC3_RuntimeMethod_var);
		return L_20;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCamera/<GetVideoThumbnailAsync>d__30::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetVideoThumbnailAsyncU3Ed__30_MoveNext_mDBB890CADFAADFBF703E0257D74428E64FE6FDA6 (U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m4B4C82CEEADEF9B96CFD0819E9DA9DC931389FE4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m850BD17D7C2BD7A37C5B5261B69E44A451533191_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_GetResult_mE4B8867B0D8DAA1317AD64FE09FBD26E825A654C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_get_IsCompleted_m77FF413EE49A5859C0BC111104448D64F3C01911_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_1_GetAwaiter_m88AFED53B032F7EDDB6F9746699970B9FFFC992C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass30_0_U3CGetVideoThumbnailAsyncU3Eb__0_m5DAF2A26314559841D948DC75B130034596DF868_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* V_1 = NULL;
	String_t* V_2 = NULL;
	TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 V_3;
	memset((&V_3), 0, sizeof(V_3));
	TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B V_4;
	memset((&V_4), 0, sizeof(V_4));
	Exception_t* V_5 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		int32_t L_0 = __this->___U3CU3E1__state_0;
		V_0 = L_0;
	}
	try
	{// begin try (depth: 1)
		{
			int32_t L_1 = V_0;
			if (!L_1)
			{
				goto IL_00c8_1;
			}
		}
		{
			int32_t L_2 = V_0;
			if ((((int32_t)L_2) == ((int32_t)1)))
			{
				goto IL_013c_1;
			}
		}
		{
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_3 = (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A_il2cpp_TypeInfo_var);
			NullCheck(L_3);
			U3CU3Ec__DisplayClass30_0__ctor_m1FE9FD5C2A2C59BAB3B1BD833EE7F70A5D49DACE(L_3, NULL);
			__this->___U3CU3E8__1_5 = L_3;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)L_3);
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_4 = __this->___U3CU3E8__1_5;
			String_t* L_5 = __this->___videoPath_2;
			NullCheck(L_4);
			L_4->___videoPath_0 = L_5;
			Il2CppCodeGenWriteBarrier((void**)(&L_4->___videoPath_0), (void*)L_5);
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_6 = __this->___U3CU3E8__1_5;
			int32_t L_7 = __this->___maxSize_3;
			NullCheck(L_6);
			L_6->___maxSize_2 = L_7;
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_8 = __this->___U3CU3E8__1_5;
			double L_9 = __this->___captureTimeInSeconds_4;
			NullCheck(L_8);
			L_8->___captureTimeInSeconds_3 = L_9;
			// if( maxSize <= 0 )
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_10 = __this->___U3CU3E8__1_5;
			NullCheck(L_10);
			int32_t L_11 = L_10->___maxSize_2;
			if ((((int32_t)L_11) > ((int32_t)0)))
			{
				goto IL_0070_1;
			}
		}
		{
			// maxSize = SystemInfo.maxTextureSize;
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_12 = __this->___U3CU3E8__1_5;
			int32_t L_13;
			L_13 = SystemInfo_get_maxTextureSize_mEE557C09643222591C6F4D3F561D7A60CD403991(NULL);
			NullCheck(L_12);
			L_12->___maxSize_2 = L_13;
		}

IL_0070_1:
		{
			// string temporaryImagePath = TemporaryImagePath; // Must be accessed from main thread
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_14 = __this->___U3CU3E8__1_5;
			String_t* L_15;
			L_15 = NativeCamera_get_TemporaryImagePath_m8697D60DFA6387A8227ABEE74C34C46FDC14E027(NULL);
			NullCheck(L_14);
			L_14->___temporaryImagePath_1 = L_15;
			Il2CppCodeGenWriteBarrier((void**)(&L_14->___temporaryImagePath_1), (void*)L_15);
			// string thumbnailPath = await TryCallNativeAndroidFunctionOnSeparateThread( () => AJC.CallStatic<string>( "GetVideoThumbnail", Context, videoPath, temporaryImagePath + ".png", false, maxSize, captureTimeInSeconds ) );
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_16 = __this->___U3CU3E8__1_5;
			Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C* L_17 = (Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C*)il2cpp_codegen_object_new(Func_1_t367387BB2C476D3F32DB12161B5FDC128DC3231C_il2cpp_TypeInfo_var);
			NullCheck(L_17);
			Func_1__ctor_m27A68E928C1D9158EAAD261086B9BC424339327B(L_17, L_16, (intptr_t)((void*)U3CU3Ec__DisplayClass30_0_U3CGetVideoThumbnailAsyncU3Eb__0_m5DAF2A26314559841D948DC75B130034596DF868_RuntimeMethod_var), NULL);
			Task_1_t3D7638C82ED289AF156EDBAE76842D8DF4C4A9E0* L_18;
			L_18 = NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC(L_17, NativeCamera_TryCallNativeAndroidFunctionOnSeparateThread_TisString_t_m2AAA22C4B280E4D6C823F92ECDD3A395F62AFEEC_RuntimeMethod_var);
			NullCheck(L_18);
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_19;
			L_19 = Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F(L_18, Task_1_GetAwaiter_m7727657658441E9D4CE9D3F8B532F9D65CB9CE1F_RuntimeMethod_var);
			V_3 = L_19;
			bool L_20;
			L_20 = TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8((&V_3), TaskAwaiter_1_get_IsCompleted_mE207C5509602B0BB59366E53CED6CC9B10A1F8A8_RuntimeMethod_var);
			if (L_20)
			{
				goto IL_00e4_1;
			}
		}
		{
			int32_t L_21 = 0;
			V_0 = L_21;
			__this->___U3CU3E1__state_0 = L_21;
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_22 = V_3;
			__this->___U3CU3Eu__1_7 = L_22;
			Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CU3Eu__1_7))->___m_task_0), (void*)NULL);
			AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_23 = (&__this->___U3CU3Et__builder_1);
			AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m4B4C82CEEADEF9B96CFD0819E9DA9DC931389FE4(L_23, (&V_3), __this, AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m4B4C82CEEADEF9B96CFD0819E9DA9DC931389FE4_RuntimeMethod_var);
			goto IL_01a2;
		}

IL_00c8_1:
		{
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6 L_24 = __this->___U3CU3Eu__1_7;
			V_3 = L_24;
			TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6* L_25 = (&__this->___U3CU3Eu__1_7);
			il2cpp_codegen_initobj(L_25, sizeof(TaskAwaiter_1_t254638BB1FAD695D9A9542E098A189D438A000F6));
			int32_t L_26 = (-1);
			V_0 = L_26;
			__this->___U3CU3E1__state_0 = L_26;
		}

IL_00e4_1:
		{
			String_t* L_27;
			L_27 = TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9((&V_3), TaskAwaiter_1_GetResult_m82A392802A854576DC9525B87B0053B56201ABB9_RuntimeMethod_var);
			V_2 = L_27;
			// if( !string.IsNullOrEmpty( thumbnailPath ) )
			String_t* L_28 = V_2;
			bool L_29;
			L_29 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_28, NULL);
			if (L_29)
			{
				goto IL_0163_1;
			}
		}
		{
			// return await LoadImageAtPathAsync( thumbnailPath, maxSize, markTextureNonReadable );
			String_t* L_30 = V_2;
			U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A* L_31 = __this->___U3CU3E8__1_5;
			NullCheck(L_31);
			int32_t L_32 = L_31->___maxSize_2;
			bool L_33 = __this->___markTextureNonReadable_6;
			Task_1_t95921EB64E237ACD28589D64B693C652268F225E* L_34;
			L_34 = NativeCamera_LoadImageAtPathAsync_m7B912F60F0DF9C36558976E4F53701CF76073FC0(L_30, L_32, L_33, NULL);
			NullCheck(L_34);
			TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B L_35;
			L_35 = Task_1_GetAwaiter_m88AFED53B032F7EDDB6F9746699970B9FFFC992C(L_34, Task_1_GetAwaiter_m88AFED53B032F7EDDB6F9746699970B9FFFC992C_RuntimeMethod_var);
			V_4 = L_35;
			bool L_36;
			L_36 = TaskAwaiter_1_get_IsCompleted_m77FF413EE49A5859C0BC111104448D64F3C01911((&V_4), TaskAwaiter_1_get_IsCompleted_m77FF413EE49A5859C0BC111104448D64F3C01911_RuntimeMethod_var);
			if (L_36)
			{
				goto IL_0159_1;
			}
		}
		{
			int32_t L_37 = 1;
			V_0 = L_37;
			__this->___U3CU3E1__state_0 = L_37;
			TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B L_38 = V_4;
			__this->___U3CU3Eu__2_8 = L_38;
			Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CU3Eu__2_8))->___m_task_0), (void*)NULL);
			AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_39 = (&__this->___U3CU3Et__builder_1);
			AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m850BD17D7C2BD7A37C5B5261B69E44A451533191(L_39, (&V_4), __this, AsyncTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisTaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B_TisU3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0_m850BD17D7C2BD7A37C5B5261B69E44A451533191_RuntimeMethod_var);
			goto IL_01a2;
		}

IL_013c_1:
		{
			TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B L_40 = __this->___U3CU3Eu__2_8;
			V_4 = L_40;
			TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B* L_41 = (&__this->___U3CU3Eu__2_8);
			il2cpp_codegen_initobj(L_41, sizeof(TaskAwaiter_1_t6D65E8305E8A65AA4939559CBC191F5C2238F74B));
			int32_t L_42 = (-1);
			V_0 = L_42;
			__this->___U3CU3E1__state_0 = L_42;
		}

IL_0159_1:
		{
			Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_43;
			L_43 = TaskAwaiter_1_GetResult_mE4B8867B0D8DAA1317AD64FE09FBD26E825A654C((&V_4), TaskAwaiter_1_GetResult_mE4B8867B0D8DAA1317AD64FE09FBD26E825A654C_RuntimeMethod_var);
			V_1 = L_43;
			goto IL_0187;
		}

IL_0163_1:
		{
			// return null;
			V_1 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)NULL;
			goto IL_0187;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0167;
		}
		throw e;
	}

CATCH_0167:
	{// begin catch(System.Exception)
		V_5 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));
		__this->___U3CU3E1__state_0 = ((int32_t)-2);
		__this->___U3CU3E8__1_5 = (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)(U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A*)NULL);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_44 = (&__this->___U3CU3Et__builder_1);
		Exception_t* L_45 = V_5;
		AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B(L_44, L_45, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AsyncTaskMethodBuilder_1_SetException_m1697D9F7BF9D11383EDE12CEE54A18AC24A7786B_RuntimeMethod_var)));
		IL2CPP_POP_ACTIVE_EXCEPTION();
		goto IL_01a2;
	}// end catch (depth: 1)

IL_0187:
	{
		// }
		__this->___U3CU3E1__state_0 = ((int32_t)-2);
		__this->___U3CU3E8__1_5 = (U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E8__1_5), (void*)(U3CU3Ec__DisplayClass30_0_t60C21183057F871D199E7A529924F58CC2FFAB2A*)NULL);
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_46 = (&__this->___U3CU3Et__builder_1);
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_47 = V_1;
		AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688(L_46, L_47, AsyncTaskMethodBuilder_1_SetResult_mAB04B95B4490AB6E1FCB475391576D15606A2688_RuntimeMethod_var);
	}

IL_01a2:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void U3CGetVideoThumbnailAsyncU3Ed__30_MoveNext_mDBB890CADFAADFBF703E0257D74428E64FE6FDA6_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0*>(__this + _offset);
	U3CGetVideoThumbnailAsyncU3Ed__30_MoveNext_mDBB890CADFAADFBF703E0257D74428E64FE6FDA6(_thisAdjusted, method);
}
// System.Void NativeCamera/<GetVideoThumbnailAsync>d__30::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetVideoThumbnailAsyncU3Ed__30_SetStateMachine_m3D9A0912DB18DF40ACC898395143BBF274B31AEB (U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		AsyncTaskMethodBuilder_1_t05B41371EF0E362ABCB54C56063FAA1E00C69A8F* L_0 = (&__this->___U3CU3Et__builder_1);
		RuntimeObject* L_1 = ___stateMachine0;
		AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD(L_0, L_1, AsyncTaskMethodBuilder_1_SetStateMachine_m0ECA1B3B604CFFB8A5DE544E2A0A0025BFE6B6FD_RuntimeMethod_var);
		return;
	}
}
IL2CPP_EXTERN_C  void U3CGetVideoThumbnailAsyncU3Ed__30_SetStateMachine_m3D9A0912DB18DF40ACC898395143BBF274B31AEB_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___stateMachine0, const RuntimeMethod* method)
{
	U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CGetVideoThumbnailAsyncU3Ed__30_tAC754D082B2D3C667BEED95E73E4FAB32F5D8BB0*>(__this + _offset);
	U3CGetVideoThumbnailAsyncU3Ed__30_SetStateMachine_m3D9A0912DB18DF40ACC898395143BBF274B31AEB(_thisAdjusted, ___stateMachine0, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCameraNamespace.NCCallbackHelper::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCallbackHelper_Awake_mE3A63B5B9F2BF8D9380A10938C023518513C42DA (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// DontDestroyOnLoad( gameObject );
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_DontDestroyOnLoad_m4B70C3AEF886C176543D1295507B6455C9DCAEA7(L_0, NULL);
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCCallbackHelper::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCallbackHelper_Update_mB751E20E799C93573FD3AF0BE18DECA33BAA6A9C (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if( mainThreadAction != null )
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = __this->___mainThreadAction_4;
		if (!L_0)
		{
			goto IL_0028;
		}
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_001c:
			{// begin finally (depth: 1)
				// Destroy( gameObject );
				GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_1;
				L_1 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				Object_Destroy_mE97D0A766419A81296E8D4E5C23D01D3FE91ACBB(L_1, NULL);
				// }
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			// System.Action temp = mainThreadAction;
			Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = __this->___mainThreadAction_4;
			// mainThreadAction = null;
			__this->___mainThreadAction_4 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
			Il2CppCodeGenWriteBarrier((void**)(&__this->___mainThreadAction_4), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
			// temp();
			NullCheck(L_2);
			Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_2, NULL);
			// }
			goto IL_0028;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0028:
	{
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCCallbackHelper::CallOnMainThread(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCallbackHelper_CallOnMainThread_mF4C7C68F199FEE8034F6E1EFFE293DB4D92D5192 (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___function0, const RuntimeMethod* method) 
{
	{
		// mainThreadAction = function;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ___function0;
		__this->___mainThreadAction_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mainThreadAction_4), (void*)L_0);
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCCallbackHelper::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCallbackHelper__ctor_m079DDE184188CCFE394DA8337BB081D1E9E087A5 (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid::.ctor(NativeCamera/CameraCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCameraCallbackAndroid__ctor_m7A49A723EBFBFC7215432493733AFCF632A8F5AA (NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* __this, CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* ___callback0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0A051229B6EA622EA0A0C01F91FB09E1623476AA);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral22965B1EE1F6EB152925F32087BD48959BA8C1BC);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public NCCameraCallbackAndroid( NativeCamera.CameraCallback callback ) : base( "com.yasirkula.unity.NativeCameraMediaReceiver" )
		il2cpp_codegen_runtime_class_init_inline(AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7(__this, _stringLiteral0A051229B6EA622EA0A0C01F91FB09E1623476AA, NULL);
		// this.callback = callback;
		CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* L_0 = ___callback0;
		__this->___callback_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___callback_4), (void*)L_0);
		// callbackHelper = new GameObject( "NCCallbackHelper" ).AddComponent<NCCallbackHelper>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_1 = (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*)il2cpp_codegen_object_new(GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		GameObject__ctor_m37D512B05D292F954792225E6C6EEE95293A9B88(L_1, _stringLiteral22965B1EE1F6EB152925F32087BD48959BA8C1BC, NULL);
		NullCheck(L_1);
		NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* L_2;
		L_2 = GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE(L_1, GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE_RuntimeMethod_var);
		__this->___callbackHelper_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___callbackHelper_5), (void*)L_2);
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid::OnMediaReceived(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCCameraCallbackAndroid_OnMediaReceived_mA794170C46D6C0CB63A52483CDA830D08530E62A (NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* __this, String_t* ___path0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass3_0_U3COnMediaReceivedU3Eb__0_m2F42366AEBB844734D6B392AF6A392BF2BF19458_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* L_0 = (U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass3_0__ctor_mAEE97D12D327C8C28EAB45ACDCB50E5DC04F50FB(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3E4__this_0 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3E4__this_0), (void*)__this);
		U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* L_2 = V_0;
		String_t* L_3 = ___path0;
		NullCheck(L_2);
		L_2->___path_1 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___path_1), (void*)L_3);
		// callbackHelper.CallOnMainThread( () => callback( !string.IsNullOrEmpty( path ) ? path : null ) );
		NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* L_4 = __this->___callbackHelper_5;
		U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* L_5 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_6, L_5, (intptr_t)((void*)U3CU3Ec__DisplayClass3_0_U3COnMediaReceivedU3Eb__0_m2F42366AEBB844734D6B392AF6A392BF2BF19458_RuntimeMethod_var), NULL);
		NullCheck(L_4);
		NCCallbackHelper_CallOnMainThread_mF4C7C68F199FEE8034F6E1EFFE293DB4D92D5192_inline(L_4, L_6, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0__ctor_mAEE97D12D327C8C28EAB45ACDCB50E5DC04F50FB (U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void NativeCameraNamespace.NCCameraCallbackAndroid/<>c__DisplayClass3_0::<OnMediaReceived>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0_U3COnMediaReceivedU3Eb__0_m2F42366AEBB844734D6B392AF6A392BF2BF19458 (U3CU3Ec__DisplayClass3_0_t6F61CA2BBD832CB410C9365B047308C3CAC6B850* __this, const RuntimeMethod* method) 
{
	CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* G_B2_0 = NULL;
	CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* G_B1_0 = NULL;
	String_t* G_B3_0 = NULL;
	CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* G_B3_1 = NULL;
	{
		// callbackHelper.CallOnMainThread( () => callback( !string.IsNullOrEmpty( path ) ? path : null ) );
		NCCameraCallbackAndroid_t6321AB2CC0C23E0A0A06CD0D72847CE9149F9378* L_0 = __this->___U3CU3E4__this_0;
		NullCheck(L_0);
		CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* L_1 = L_0->___callback_4;
		String_t* L_2 = __this->___path_1;
		bool L_3;
		L_3 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_2, NULL);
		G_B1_0 = L_1;
		if (!L_3)
		{
			G_B2_0 = L_1;
			goto IL_001b;
		}
	}
	{
		G_B3_0 = ((String_t*)(NULL));
		G_B3_1 = G_B1_0;
		goto IL_0021;
	}

IL_001b:
	{
		String_t* L_4 = __this->___path_1;
		G_B3_0 = L_4;
		G_B3_1 = G_B2_0;
	}

IL_0021:
	{
		NullCheck(G_B3_1);
		CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_inline(G_B3_1, G_B3_0, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Int32 NativeCameraNamespace.NCPermissionCallbackAndroid::get_Result()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, const RuntimeMethod* method) 
{
	{
		// public int Result { get; private set; }
		int32_t L_0 = __this->___U3CResultU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Void NativeCameraNamespace.NCPermissionCallbackAndroid::set_Result(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid_set_Result_m875E1D987FD8099207FD71BC8FCCE424DEA1DC6A (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public int Result { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3CResultU3Ek__BackingField_5 = L_0;
		return;
	}
}
// System.Void NativeCameraNamespace.NCPermissionCallbackAndroid::.ctor(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid__ctor_mEE399CA43702611369BBFD6E72E3147E4C962FDC (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, RuntimeObject* ___threadLock0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF0750A05DD548EB8E0F9CD56A424497F03563B9D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public NCPermissionCallbackAndroid( object threadLock ) : base( "com.yasirkula.unity.NativeCameraPermissionReceiver" )
		il2cpp_codegen_runtime_class_init_inline(AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7(__this, _stringLiteralF0750A05DD548EB8E0F9CD56A424497F03563B9D, NULL);
		// Result = -1;
		NCPermissionCallbackAndroid_set_Result_m875E1D987FD8099207FD71BC8FCCE424DEA1DC6A_inline(__this, (-1), NULL);
		// this.threadLock = threadLock;
		RuntimeObject* L_0 = ___threadLock0;
		__this->___threadLock_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___threadLock_4), (void*)L_0);
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCPermissionCallbackAndroid::OnPermissionResult(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid_OnPermissionResult_m1133986BD48E36D9A799B42BD73C67EC51D3319D (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, int32_t ___result0, const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	bool V_1 = false;
	{
		// Result = result;
		int32_t L_0 = ___result0;
		NCPermissionCallbackAndroid_set_Result_m875E1D987FD8099207FD71BC8FCCE424DEA1DC6A_inline(__this, L_0, NULL);
		// lock( threadLock )
		RuntimeObject* L_1 = __this->___threadLock_4;
		V_0 = L_1;
		V_1 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0025:
			{// begin finally (depth: 1)
				{
					bool L_2 = V_1;
					if (!L_2)
					{
						goto IL_002e;
					}
				}
				{
					RuntimeObject* L_3 = V_0;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_3, NULL);
				}

IL_002e:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			RuntimeObject* L_4 = V_0;
			Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_4, (&V_1), NULL);
			// Monitor.Pulse( threadLock );
			RuntimeObject* L_5 = __this->___threadLock_4;
			Monitor_Pulse_mA8279943D6C2913ADFAF661E35C4951BDFACE43D(L_5, NULL);
			// }
			goto IL_002f;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_002f:
	{
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid::.ctor(NativeCamera/PermissionCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAsyncAndroid__ctor_m148D3C8505CACDED882A222F2081610060ACF67F (NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* __this, PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* ___callback0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral22965B1EE1F6EB152925F32087BD48959BA8C1BC);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF0750A05DD548EB8E0F9CD56A424497F03563B9D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public NCPermissionCallbackAsyncAndroid( NativeCamera.PermissionCallback callback ) : base( "com.yasirkula.unity.NativeCameraPermissionReceiver" )
		il2cpp_codegen_runtime_class_init_inline(AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7(__this, _stringLiteralF0750A05DD548EB8E0F9CD56A424497F03563B9D, NULL);
		// this.callback = callback;
		PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* L_0 = ___callback0;
		__this->___callback_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___callback_4), (void*)L_0);
		// callbackHelper = new GameObject( "NCCallbackHelper" ).AddComponent<NCCallbackHelper>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_1 = (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*)il2cpp_codegen_object_new(GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		GameObject__ctor_m37D512B05D292F954792225E6C6EEE95293A9B88(L_1, _stringLiteral22965B1EE1F6EB152925F32087BD48959BA8C1BC, NULL);
		NullCheck(L_1);
		NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* L_2;
		L_2 = GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE(L_1, GameObject_AddComponent_TisNCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA_m58F9A27F8E2E59C5DA67176DBC1DDEE4B50743EE_RuntimeMethod_var);
		__this->___callbackHelper_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___callbackHelper_5), (void*)L_2);
		// }
		return;
	}
}
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid::OnPermissionResult(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NCPermissionCallbackAsyncAndroid_OnPermissionResult_m6B0F4D6DF5E150921526D15C51C80583166F62D5 (NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* __this, int32_t ___result0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass3_0_U3COnPermissionResultU3Eb__0_m18426273EFEC1619EDB4B879CA210DD0957D2796_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* L_0 = (U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass3_0__ctor_mD3F7CC1873EDBE61E4E200563157CFB5895A4563(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3E4__this_0 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3E4__this_0), (void*)__this);
		U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* L_2 = V_0;
		int32_t L_3 = ___result0;
		NullCheck(L_2);
		L_2->___result_1 = L_3;
		// callbackHelper.CallOnMainThread( () => callback( (NativeCamera.Permission) result ) );
		NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* L_4 = __this->___callbackHelper_5;
		U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* L_5 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_6, L_5, (intptr_t)((void*)U3CU3Ec__DisplayClass3_0_U3COnPermissionResultU3Eb__0_m18426273EFEC1619EDB4B879CA210DD0957D2796_RuntimeMethod_var), NULL);
		NullCheck(L_4);
		NCCallbackHelper_CallOnMainThread_mF4C7C68F199FEE8034F6E1EFFE293DB4D92D5192_inline(L_4, L_6, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0__ctor_mD3F7CC1873EDBE61E4E200563157CFB5895A4563 (U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void NativeCameraNamespace.NCPermissionCallbackAsyncAndroid/<>c__DisplayClass3_0::<OnPermissionResult>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass3_0_U3COnPermissionResultU3Eb__0_m18426273EFEC1619EDB4B879CA210DD0957D2796 (U3CU3Ec__DisplayClass3_0_t319D4DE1A7D6F305652D5D71076DAF715F3E8B32* __this, const RuntimeMethod* method) 
{
	{
		// callbackHelper.CallOnMainThread( () => callback( (NativeCamera.Permission) result ) );
		NCPermissionCallbackAsyncAndroid_t08CB3D1F1CFC48A9095CDF5D34D1947C97A27B8B* L_0 = __this->___U3CU3E4__this_0;
		NullCheck(L_0);
		PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* L_1 = L_0->___callback_4;
		int32_t L_2 = __this->___result_1;
		NullCheck(L_1);
		PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_inline(L_1, L_2, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NCPermissionCallbackAndroid_get_Result_m0FBAA6C84093FCD7833C957E5C7CE381668100DF_inline (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, const RuntimeMethod* method) 
{
	{
		// public int Result { get; private set; }
		int32_t L_0 = __this->___U3CResultU3Ek__BackingField_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NCCallbackHelper_CallOnMainThread_mF4C7C68F199FEE8034F6E1EFFE293DB4D92D5192_inline (NCCallbackHelper_tBA518C908F8A8D4DD1D896CC9C11AE23888F67CA* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___function0, const RuntimeMethod* method) 
{
	{
		// mainThreadAction = function;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ___function0;
		__this->___mainThreadAction_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mainThreadAction_4), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CameraCallback_Invoke_mE8BD077A2F42B9DBC93521F630A1461381D3BB1A_inline (CameraCallback_tF33A274B891CCEC46FF206A2C3907F370B4FF771* __this, String_t* ___path0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___path0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NCPermissionCallbackAndroid_set_Result_m875E1D987FD8099207FD71BC8FCCE424DEA1DC6A_inline (NCPermissionCallbackAndroid_t018A82939BD516F7AC1B1295721A0EDB10B55D91* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public int Result { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3CResultU3Ek__BackingField_5 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PermissionCallback_Invoke_m0D631ECACFB70385F4288017D881CFB459CACA47_inline (PermissionCallback_tF9BB768D4EF01EF761635D5227FC907EFC8EA001* __this, int32_t ___permission0, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___permission0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB* TaskCompletionSource_1_get_Task_mB4A2FF75AC28BB6E3B7A55129E9CD347E5F06FDC_gshared_inline (TaskCompletionSource_1_tF8DA32849B904AE4F51ECAF6C6D7FA080481A35A* __this, const RuntimeMethod* method) 
{
	{
		Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB* L_0 = (Task_1_t8DED34447688BFCF5112B0D05D5A80CED94E4BFB*)__this->____task_0;
		return L_0;
	}
}
