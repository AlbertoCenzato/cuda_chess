#pragma once

#include <cstdint>

namespace chess {

	struct Move {
		std::uint64_t from;
		std::uint64_t to;
		std::int32_t val;
	};


}