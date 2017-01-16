v8 Support Library
==================

This library is designed to be used with Android (API level 8) and higher. It adds support for the RenderScript computation framework.

RenderScript is a framework for running computationally intensive tasks at high performance on Android. RenderScript is primarily oriented for use with data-parallel computation, although serial computationally intensive workloads can benefit as well. The RenderScript runtime will parallelize work across all processors available on a device, such as multi-core CPUs, GPUs, or DSPs, allowing you to focus on expressing algorithms rather than scheduling work or load balancing. RenderScript is especially useful for applications performing image processing, computational photography, or computer vision.

The renderscript library won't run on armv6 (armeabi), so make sure that you are only supporting either armeabi-v7a, mips or x86.

There are several APIs included in this library. For complete, detailed information about the v8 Support Library APIs, [see the android.support.v8.renderscript](http://developer.android.com/reference/android/support/v8/renderscript/package-summary.html).

*Portions of this page are modifications based on [work][3] created and [shared by the Android Open Source Project][1] and used according to terms described in the [Creative Commons 2.5 Attribution License][2].*

[1]: http://code.google.com/policies.html
[2]: http://creativecommons.org/licenses/by/2.5/
[3]: http://developer.android.com/tools/support-library/features.html
