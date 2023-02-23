#include <gtest/gtest.h>
#include "../src/core.hpp"

TEST(test_core, getTickCount) {
    int64 value = 0;
    ASSERT_EQ(
        core_getTickCount(&value), 
        ExceptionStatus::NotOccurred);

    ASSERT_NE(value, 0);
}

TEST(test_core, getBuildInformation) {
    std::string buf;
    ASSERT_EQ(
        core_getBuildInformation(&buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}
