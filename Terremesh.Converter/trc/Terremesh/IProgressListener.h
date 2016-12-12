#pragma once
#ifndef _Terremesh_IProgressListener_H__
#define _Terremesh_IProgressListener_H__

#include "Required.h"

namespace Terremesh
{
	/// Provides interface for advancing computation progress.
	struct IProgressListener
	{
		virtual ~IProgressListener() {}

		/// Starts listener stage.
		///
		/// @param[in] stage
		///		The listener stage.
		virtual void OnStarted(const std::string& stage) = 0;

		/// Ends listener stage.
		///
		/// @param[in] stage
		///		The listener stage.
		virtual void OnCompleted(const std::string& stage) = 0;

		/// Advances listener progress.
		///
		/// @param[in] current
		///		The current progress value.
		/// @param[in] total
		///		The total number of steps.
		virtual void OnStep(int current, int total) = 0;
	};
}

#endif /* _Terremesh_IProgressListener_H__ */