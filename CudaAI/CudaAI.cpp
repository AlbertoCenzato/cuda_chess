// CudaAI.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#include "CudaAI.h"

chess::Move next_move() {
	chess::Move move;
	move.from = 1L;
	move.to = 1024L;
	move.val = 42;
	return move;
}