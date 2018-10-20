#pragma once

#ifdef CUDAAI_EXPORTS
#define CUDAAI_API __declspec(dllexport)
#else
#define CUDAAI_API __declspec(dllimport)
#endif

#include <string>

#include "CommonTypes.h"


extern "C" CUDAAI_API int special_number();

extern "C" CUDAAI_API std::string hello_world();

extern "C" CUDAAI_API chess::Move next_move();