#include <gtest/gtest.h>
#include "../src/core.hpp"

TEST(test_core, getTickCount) {
    core_getTickCount();
}

TEST(test_core, getBuildInformation) {
    std::string buf;
    core_getBuildInformation(&buf);

    ASSERT_FALSE(buf.empty());
}
