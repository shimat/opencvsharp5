#include <gtest/gtest.h>
#include "../src/core.hpp"

TEST(test_getTickCount, success) {
    core_getTickCount();
}
