; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [127 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [254 x i32] [
	i32 42639949, ; 0: System.Threading.Thread => 0x28aa24d => 118
	i32 67008169, ; 1: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 2: Microsoft.Maui.Graphics.dll => 0x44bb714 => 48
	i32 117431740, ; 3: System.Runtime.InteropServices => 0x6ffddbc => 111
	i32 182336117, ; 4: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 78
	i32 195452805, ; 5: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 6: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 7: System.ComponentModel => 0xc38ff48 => 95
	i32 280992041, ; 8: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 317674968, ; 9: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 10: Xamarin.AndroidX.Activity.dll => 0x13031348 => 55
	i32 336156722, ; 11: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 12: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 66
	i32 347068432, ; 13: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 53
	i32 356389973, ; 14: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 379916513, ; 15: System.Threading.Thread.dll => 0x16a510e1 => 118
	i32 385762202, ; 16: System.Memory.dll => 0x16fe439a => 102
	i32 395744057, ; 17: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 435591531, ; 18: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 19: System.Collections => 0x1a61054f => 92
	i32 450948140, ; 20: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 65
	i32 469710990, ; 21: System.dll => 0x1bff388e => 121
	i32 498788369, ; 22: System.ObjectModel => 0x1dbae811 => 107
	i32 500358224, ; 23: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 24: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 504143952, ; 25: Plugin.LocalNotification.dll => 0x1e0ca050 => 49
	i32 513247710, ; 26: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 43
	i32 539058512, ; 27: Microsoft.Extensions.Logging => 0x20216150 => 40
	i32 592146354, ; 28: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 627609679, ; 29: Xamarin.AndroidX.CustomView => 0x2568904f => 63
	i32 627931235, ; 30: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 31: System.Text.Encodings.Web.dll => 0x27787397 => 115
	i32 663517072, ; 32: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 79
	i32 672442732, ; 33: System.Collections.Concurrent => 0x2814a96c => 90
	i32 688181140, ; 34: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 706645707, ; 35: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 36: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 37: System.Runtime.Loader.dll => 0x2b15ed29 => 112
	i32 748832960, ; 38: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 51
	i32 759454413, ; 39: System.Net.Requests => 0x2d445acd => 105
	i32 775507847, ; 40: System.IO.Compression => 0x2e394f87 => 99
	i32 777317022, ; 41: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 42: Microsoft.Extensions.Options => 0x2f0980eb => 42
	i32 823281589, ; 43: System.Private.Uri.dll => 0x311247b5 => 108
	i32 830298997, ; 44: System.IO.Compression.Brotli => 0x317d5b75 => 98
	i32 904024072, ; 45: System.ComponentModel.Primitives.dll => 0x35e25008 => 93
	i32 926902833, ; 46: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 967690846, ; 47: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 66
	i32 992768348, ; 48: System.Collections.dll => 0x3b2c715c => 92
	i32 1012816738, ; 49: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 77
	i32 1028951442, ; 50: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 39
	i32 1029334545, ; 51: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 52: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 56
	i32 1044663988, ; 53: System.Linq.Expressions.dll => 0x3e444eb4 => 100
	i32 1082857460, ; 54: System.ComponentModel.TypeConverter => 0x408b17f4 => 94
	i32 1084122840, ; 55: Xamarin.Kotlin.StdLib => 0x409e66d8 => 87
	i32 1098259244, ; 56: System => 0x41761b2c => 121
	i32 1118262833, ; 57: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1168523401, ; 58: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 59: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 74
	i32 1203215381, ; 60: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1214827643, ; 61: CommunityToolkit.Mvvm => 0x4868cc7b => 35
	i32 1234928153, ; 62: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1246548578, ; 63: Xamarin.AndroidX.Collection.Jvm.dll => 0x4a4cd262 => 59
	i32 1260983243, ; 64: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1292207520, ; 65: SQLitePCLRaw.core.dll => 0x4d0585a0 => 52
	i32 1293217323, ; 66: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 64
	i32 1324164729, ; 67: System.Linq => 0x4eed2679 => 101
	i32 1373134921, ; 68: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 69: Xamarin.AndroidX.SavedState => 0x52114ed3 => 77
	i32 1406073936, ; 70: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 60
	i32 1430672901, ; 71: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1437299793, ; 72: Xamarin.AndroidX.Lifecycle.Common.Jvm => 0x55ab7451 => 67
	i32 1441095154, ; 73: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 0x55e55df2 => 69
	i32 1461004990, ; 74: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1462112819, ; 75: System.IO.Compression.dll => 0x57261233 => 99
	i32 1469204771, ; 76: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 57
	i32 1470490898, ; 77: Microsoft.Extensions.Primitives => 0x57a5e912 => 43
	i32 1480492111, ; 78: System.IO.Compression.Brotli.dll => 0x583e844f => 98
	i32 1493001747, ; 79: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1514721132, ; 80: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1524747670, ; 81: Plugin.LocalNotification => 0x5ae1cd96 => 49
	i32 1543031311, ; 82: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 117
	i32 1551623176, ; 83: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 84: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 71
	i32 1624863272, ; 85: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 81
	i32 1636350590, ; 86: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 62
	i32 1639515021, ; 87: System.Net.Http.dll => 0x61b9038d => 103
	i32 1639986890, ; 88: System.Text.RegularExpressions => 0x61c036ca => 117
	i32 1657153582, ; 89: System.Runtime => 0x62c6282e => 113
	i32 1658251792, ; 90: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 82
	i32 1677501392, ; 91: System.Net.Primitives.dll => 0x63fca3d0 => 104
	i32 1679769178, ; 92: System.Security.Cryptography => 0x641f3e5a => 114
	i32 1711441057, ; 93: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 53
	i32 1729485958, ; 94: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 58
	i32 1736233607, ; 95: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
	i32 1743415430, ; 96: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1766324549, ; 97: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 78
	i32 1770582343, ; 98: Microsoft.Extensions.Logging.dll => 0x6988f147 => 40
	i32 1780572499, ; 99: Mono.Android.Runtime.dll => 0x6a216153 => 125
	i32 1782862114, ; 100: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 101: Xamarin.AndroidX.Fragment => 0x6a96652d => 65
	i32 1793755602, ; 102: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 103: Xamarin.AndroidX.Loader => 0x6bcd3296 => 71
	i32 1813058853, ; 104: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 87
	i32 1813201214, ; 105: Xamarin.Google.Android.Material => 0x6c13413e => 82
	i32 1818569960, ; 106: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 75
	i32 1828688058, ; 107: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 41
	i32 1842015223, ; 108: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 109: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 110: System.Linq.Expressions => 0x6ec71a65 => 100
	i32 1875935024, ; 111: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1908813208, ; 112: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 84
	i32 1910275211, ; 113: System.Collections.NonGeneric.dll => 0x71dc7c8b => 91
	i32 1968388702, ; 114: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 36
	i32 2003115576, ; 115: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2025202353, ; 116: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
	i32 2045470958, ; 117: System.Private.Xml => 0x79eb68ee => 109
	i32 2055257422, ; 118: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 68
	i32 2066184531, ; 119: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2079903147, ; 120: System.Runtime.dll => 0x7bf8cdab => 113
	i32 2090596640, ; 121: System.Numerics.Vectors => 0x7c9bf920 => 106
	i32 2103459038, ; 122: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 54
	i32 2127167465, ; 123: System.Console => 0x7ec9ffe9 => 96
	i32 2129483829, ; 124: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 83
	i32 2159891885, ; 125: Microsoft.Maui => 0x80bd55ad => 46
	i32 2169148018, ; 126: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 127: Microsoft.Extensions.Options.dll => 0x820d22b3 => 42
	i32 2192057212, ; 128: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 41
	i32 2193016926, ; 129: System.ObjectModel.dll => 0x82b6c85e => 107
	i32 2201107256, ; 130: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 88
	i32 2201231467, ; 131: System.Net.Http => 0x8334206b => 103
	i32 2207618523, ; 132: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2266799131, ; 133: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 37
	i32 2270573516, ; 134: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 135: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 76
	i32 2303942373, ; 136: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 137: System.Private.CoreLib.dll => 0x896b7878 => 123
	i32 2340441535, ; 138: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 110
	i32 2353062107, ; 139: System.Net.Primitives => 0x8c40e0db => 104
	i32 2368005991, ; 140: System.Xml.ReaderWriter.dll => 0x8d24e767 => 120
	i32 2371007202, ; 141: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 36
	i32 2395872292, ; 142: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2427813419, ; 143: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 144: System.Console.dll => 0x912896e5 => 96
	i32 2465273461, ; 145: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 51
	i32 2467916561, ; 146: C971 Grant Putnam => 0x93196b11 => 89
	i32 2471841756, ; 147: netstandard.dll => 0x93554fdc => 122
	i32 2475788418, ; 148: Java.Interop.dll => 0x93918882 => 124
	i32 2480646305, ; 149: Microsoft.Maui.Controls => 0x93dba8a1 => 44
	i32 2550873716, ; 150: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 151: System.Text.Encodings.Web => 0x9930ee42 => 115
	i32 2593496499, ; 152: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 153: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 88
	i32 2617129537, ; 154: System.Private.Xml.dll => 0x9bfe3a41 => 109
	i32 2620871830, ; 155: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 62
	i32 2626831493, ; 156: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2663698177, ; 157: System.Runtime.Loader => 0x9ec4cf01 => 112
	i32 2732626843, ; 158: Xamarin.AndroidX.Activity => 0xa2e0939b => 55
	i32 2737747696, ; 159: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 57
	i32 2752995522, ; 160: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 161: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 45
	i32 2764765095, ; 162: Microsoft.Maui.dll => 0xa4caf7a7 => 46
	i32 2766642685, ; 163: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 0xa4e79dfd => 69
	i32 2778768386, ; 164: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 80
	i32 2780199943, ; 165: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 0xa5b67c07 => 67
	i32 2785988530, ; 166: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 167: Microsoft.Maui.Graphics => 0xa7008e0b => 48
	i32 2806116107, ; 168: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 169: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 60
	i32 2831556043, ; 170: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2847418871, ; 171: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 83
	i32 2853208004, ; 172: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 80
	i32 2861189240, ; 173: Microsoft.Maui.Essentials => 0xaa8a4878 => 47
	i32 2909740682, ; 174: System.Private.CoreLib => 0xad6f1e8a => 123
	i32 2916838712, ; 175: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 81
	i32 2919462931, ; 176: System.Numerics.Vectors.dll => 0xae037813 => 106
	i32 2959614098, ; 177: System.ComponentModel.dll => 0xb0682092 => 95
	i32 2978675010, ; 178: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 64
	i32 3038032645, ; 179: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 180: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 72
	i32 3058099980, ; 181: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 86
	i32 3059408633, ; 182: Mono.Android.Runtime => 0xb65adef9 => 125
	i32 3059793426, ; 183: System.ComponentModel.Primitives => 0xb660be12 => 93
	i32 3077302341, ; 184: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3156631223, ; 185: C971 Grant Putnam.dll => 0xbc265eb7 => 89
	i32 3178803400, ; 186: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 73
	i32 3220365878, ; 187: System.Threading => 0xbff2e236 => 119
	i32 3230466174, ; 188: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 84
	i32 3258312781, ; 189: Xamarin.AndroidX.CardView => 0xc235e84d => 58
	i32 3286872994, ; 190: SQLite-net.dll => 0xc3e9b3a2 => 50
	i32 3305363605, ; 191: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 192: System.Net.Requests.dll => 0xc5b097e4 => 105
	i32 3317135071, ; 193: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 63
	i32 3346324047, ; 194: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 74
	i32 3357674450, ; 195: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 196: System.Text.Json => 0xc82afec1 => 116
	i32 3360279109, ; 197: SQLitePCLRaw.core => 0xc849ca45 => 52
	i32 3362522851, ; 198: Xamarin.AndroidX.Core => 0xc86c06e3 => 61
	i32 3366347497, ; 199: Java.Interop => 0xc8a662e9 => 124
	i32 3374999561, ; 200: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 76
	i32 3381016424, ; 201: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 202: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 38
	i32 3430777524, ; 203: netstandard => 0xcc7d82b4 => 122
	i32 3463511458, ; 204: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 205: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 94
	i32 3476120550, ; 206: Mono.Android => 0xcf3163e6 => 126
	i32 3479583265, ; 207: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 208: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 209: System.Text.Json.dll => 0xcfbaacae => 116
	i32 3494395880, ; 210: Xamarin.GooglePlayServices.Location.dll => 0xd0483fe8 => 85
	i32 3580758918, ; 211: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 212: System.Linq.dll => 0xd715a361 => 101
	i32 3624195450, ; 213: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 110
	i32 3641597786, ; 214: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 68
	i32 3643446276, ; 215: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 216: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 73
	i32 3657292374, ; 217: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 37
	i32 3672681054, ; 218: Mono.Android.dll => 0xdae8aa5e => 126
	i32 3697841164, ; 219: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 220: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 72
	i32 3748608112, ; 221: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 97
	i32 3754567612, ; 222: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 54
	i32 3792276235, ; 223: System.Collections.NonGeneric => 0xe2098b0b => 91
	i32 3823082795, ; 224: System.Security.Cryptography.dll => 0xe3df9d2b => 114
	i32 3841636137, ; 225: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 39
	i32 3849253459, ; 226: System.Runtime.InteropServices.dll => 0xe56ef253 => 111
	i32 3876362041, ; 227: SQLite-net => 0xe70c9739 => 50
	i32 3889960447, ; 228: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 229: System.Collections.Concurrent.dll => 0xe839deed => 90
	i32 3896760992, ; 230: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 61
	i32 3910130544, ; 231: Xamarin.AndroidX.Collection.Jvm => 0xe90fdb70 => 59
	i32 3921031405, ; 232: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 79
	i32 3928044579, ; 233: System.Xml.ReaderWriter => 0xea213423 => 120
	i32 3931092270, ; 234: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 75
	i32 3955647286, ; 235: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 56
	i32 3967165417, ; 236: Xamarin.GooglePlayServices.Location => 0xec7623e9 => 85
	i32 3970018735, ; 237: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 86
	i32 3980434154, ; 238: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 239: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4025784931, ; 240: System.Memory => 0xeff49a63 => 102
	i32 4046471985, ; 241: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 45
	i32 4073602200, ; 242: System.Threading.dll => 0xf2ce3c98 => 119
	i32 4094352644, ; 243: Microsoft.Maui.Essentials.dll => 0xf40add04 => 47
	i32 4100113165, ; 244: System.Private.Uri => 0xf462c30d => 108
	i32 4102112229, ; 245: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4125707920, ; 246: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 247: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 38
	i32 4150914736, ; 248: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 249: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 70
	i32 4213026141, ; 250: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 97
	i32 4271975918, ; 251: Microsoft.Maui.Controls.dll => 0xfea12dee => 44
	i32 4274623895, ; 252: CommunityToolkit.Mvvm.dll => 0xfec99597 => 35
	i32 4292120959 ; 253: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 70
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [254 x i32] [
	i32 118, ; 0
	i32 33, ; 1
	i32 48, ; 2
	i32 111, ; 3
	i32 78, ; 4
	i32 30, ; 5
	i32 31, ; 6
	i32 95, ; 7
	i32 2, ; 8
	i32 30, ; 9
	i32 55, ; 10
	i32 15, ; 11
	i32 66, ; 12
	i32 53, ; 13
	i32 14, ; 14
	i32 118, ; 15
	i32 102, ; 16
	i32 34, ; 17
	i32 26, ; 18
	i32 92, ; 19
	i32 65, ; 20
	i32 121, ; 21
	i32 107, ; 22
	i32 13, ; 23
	i32 7, ; 24
	i32 49, ; 25
	i32 43, ; 26
	i32 40, ; 27
	i32 21, ; 28
	i32 63, ; 29
	i32 19, ; 30
	i32 115, ; 31
	i32 79, ; 32
	i32 90, ; 33
	i32 1, ; 34
	i32 16, ; 35
	i32 4, ; 36
	i32 112, ; 37
	i32 51, ; 38
	i32 105, ; 39
	i32 99, ; 40
	i32 25, ; 41
	i32 42, ; 42
	i32 108, ; 43
	i32 98, ; 44
	i32 93, ; 45
	i32 28, ; 46
	i32 66, ; 47
	i32 92, ; 48
	i32 77, ; 49
	i32 39, ; 50
	i32 3, ; 51
	i32 56, ; 52
	i32 100, ; 53
	i32 94, ; 54
	i32 87, ; 55
	i32 121, ; 56
	i32 16, ; 57
	i32 22, ; 58
	i32 74, ; 59
	i32 20, ; 60
	i32 35, ; 61
	i32 18, ; 62
	i32 59, ; 63
	i32 2, ; 64
	i32 52, ; 65
	i32 64, ; 66
	i32 101, ; 67
	i32 32, ; 68
	i32 77, ; 69
	i32 60, ; 70
	i32 0, ; 71
	i32 67, ; 72
	i32 69, ; 73
	i32 6, ; 74
	i32 99, ; 75
	i32 57, ; 76
	i32 43, ; 77
	i32 98, ; 78
	i32 10, ; 79
	i32 5, ; 80
	i32 49, ; 81
	i32 117, ; 82
	i32 25, ; 83
	i32 71, ; 84
	i32 81, ; 85
	i32 62, ; 86
	i32 103, ; 87
	i32 117, ; 88
	i32 113, ; 89
	i32 82, ; 90
	i32 104, ; 91
	i32 114, ; 92
	i32 53, ; 93
	i32 58, ; 94
	i32 23, ; 95
	i32 1, ; 96
	i32 78, ; 97
	i32 40, ; 98
	i32 125, ; 99
	i32 17, ; 100
	i32 65, ; 101
	i32 9, ; 102
	i32 71, ; 103
	i32 87, ; 104
	i32 82, ; 105
	i32 75, ; 106
	i32 41, ; 107
	i32 29, ; 108
	i32 26, ; 109
	i32 100, ; 110
	i32 8, ; 111
	i32 84, ; 112
	i32 91, ; 113
	i32 36, ; 114
	i32 5, ; 115
	i32 0, ; 116
	i32 109, ; 117
	i32 68, ; 118
	i32 4, ; 119
	i32 113, ; 120
	i32 106, ; 121
	i32 54, ; 122
	i32 96, ; 123
	i32 83, ; 124
	i32 46, ; 125
	i32 12, ; 126
	i32 42, ; 127
	i32 41, ; 128
	i32 107, ; 129
	i32 88, ; 130
	i32 103, ; 131
	i32 14, ; 132
	i32 37, ; 133
	i32 8, ; 134
	i32 76, ; 135
	i32 18, ; 136
	i32 123, ; 137
	i32 110, ; 138
	i32 104, ; 139
	i32 120, ; 140
	i32 36, ; 141
	i32 13, ; 142
	i32 10, ; 143
	i32 96, ; 144
	i32 51, ; 145
	i32 89, ; 146
	i32 122, ; 147
	i32 124, ; 148
	i32 44, ; 149
	i32 11, ; 150
	i32 115, ; 151
	i32 20, ; 152
	i32 88, ; 153
	i32 109, ; 154
	i32 62, ; 155
	i32 15, ; 156
	i32 112, ; 157
	i32 55, ; 158
	i32 57, ; 159
	i32 21, ; 160
	i32 45, ; 161
	i32 46, ; 162
	i32 69, ; 163
	i32 80, ; 164
	i32 67, ; 165
	i32 27, ; 166
	i32 48, ; 167
	i32 6, ; 168
	i32 60, ; 169
	i32 19, ; 170
	i32 83, ; 171
	i32 80, ; 172
	i32 47, ; 173
	i32 123, ; 174
	i32 81, ; 175
	i32 106, ; 176
	i32 95, ; 177
	i32 64, ; 178
	i32 34, ; 179
	i32 72, ; 180
	i32 86, ; 181
	i32 125, ; 182
	i32 93, ; 183
	i32 12, ; 184
	i32 89, ; 185
	i32 73, ; 186
	i32 119, ; 187
	i32 84, ; 188
	i32 58, ; 189
	i32 50, ; 190
	i32 7, ; 191
	i32 105, ; 192
	i32 63, ; 193
	i32 74, ; 194
	i32 24, ; 195
	i32 116, ; 196
	i32 52, ; 197
	i32 61, ; 198
	i32 124, ; 199
	i32 76, ; 200
	i32 3, ; 201
	i32 38, ; 202
	i32 122, ; 203
	i32 11, ; 204
	i32 94, ; 205
	i32 126, ; 206
	i32 24, ; 207
	i32 23, ; 208
	i32 116, ; 209
	i32 85, ; 210
	i32 31, ; 211
	i32 101, ; 212
	i32 110, ; 213
	i32 68, ; 214
	i32 28, ; 215
	i32 73, ; 216
	i32 37, ; 217
	i32 126, ; 218
	i32 33, ; 219
	i32 72, ; 220
	i32 97, ; 221
	i32 54, ; 222
	i32 91, ; 223
	i32 114, ; 224
	i32 39, ; 225
	i32 111, ; 226
	i32 50, ; 227
	i32 32, ; 228
	i32 90, ; 229
	i32 61, ; 230
	i32 59, ; 231
	i32 79, ; 232
	i32 120, ; 233
	i32 75, ; 234
	i32 56, ; 235
	i32 85, ; 236
	i32 86, ; 237
	i32 27, ; 238
	i32 9, ; 239
	i32 102, ; 240
	i32 45, ; 241
	i32 119, ; 242
	i32 47, ; 243
	i32 108, ; 244
	i32 22, ; 245
	i32 17, ; 246
	i32 38, ; 247
	i32 29, ; 248
	i32 70, ; 249
	i32 97, ; 250
	i32 44, ; 251
	i32 35, ; 252
	i32 70 ; 253
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 96b6bb65e8736e45180905177aa343f0e1854ea3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
