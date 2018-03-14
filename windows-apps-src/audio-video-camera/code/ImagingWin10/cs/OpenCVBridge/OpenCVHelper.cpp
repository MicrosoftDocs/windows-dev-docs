// <SnippetOpenCVHelperInclude>
#include "pch.h"
#include "OpenCVHelper.h"
#include "MemoryBuffer.h"
// </SnippetOpenCVHelperInclude>

// <SnippetOpenCVHelperUsing>
using namespace OpenCVBridge;
using namespace Platform;
using namespace Windows::Graphics::Imaging;
using namespace Windows::Foundation;
using namespace Microsoft::WRL;
using namespace cv;
// </SnippetOpenCVHelperUsing>

// <SnippetOpenCVHelperTryConvert>
bool OpenCVHelper::TryConvert(SoftwareBitmap^ from, Mat& convertedMat)
{
	unsigned char* pPixels = nullptr;
	unsigned int capacity = 0;
	if (!GetPointerToPixelData(from, &pPixels, &capacity))
	{
		return false;
	}

	Mat mat(from->PixelHeight,
		from->PixelWidth,
		CV_8UC4, // assume input SoftwareBitmap is BGRA8
		(void*)pPixels);
	
	// shallow copy because we want convertedMat.data = pPixels
	// don't use .copyTo or .clone
	convertedMat = mat;
	return true;
}
// </SnippetOpenCVHelperTryConvert>

// <SnippetOpenCVHelperGetPointerToPixelData>
bool OpenCVHelper::GetPointerToPixelData(SoftwareBitmap^ bitmap, unsigned char** pPixelData, unsigned int* capacity)
{
	BitmapBuffer^ bmpBuffer = bitmap->LockBuffer(BitmapBufferAccessMode::ReadWrite);
	IMemoryBufferReference^ reference = bmpBuffer->CreateReference();

	ComPtr<IMemoryBufferByteAccess> pBufferByteAccess;
	if ((reinterpret_cast<IInspectable*>(reference)->QueryInterface(IID_PPV_ARGS(&pBufferByteAccess))) != S_OK)
	{
		return false;
	}

	if (pBufferByteAccess->GetBuffer(pPixelData, capacity) != S_OK)
	{
		return false;
	}
	return true;
}
// </SnippetOpenCVHelperGetPointerToPixelData>

// <SnippetOpenCVHelperBlur>
void OpenCVHelper::Blur(SoftwareBitmap^ input, SoftwareBitmap^ output)
{
	Mat inputMat, outputMat;
	if (!(TryConvert(input, inputMat) && TryConvert(output, outputMat)))
	{
		return;
	}
	blur(inputMat, outputMat, cv::Size(15, 15));
}
// </SnippetOpenCVHelperBlur>


