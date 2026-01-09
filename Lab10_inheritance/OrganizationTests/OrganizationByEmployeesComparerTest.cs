using OrganizationLib;
using System;
using System.Collections;
using Xunit;

namespace OrganizationTests
{
    public class OrganizationByEmployeesComparerTests
    {
        // ------------------ Compare ------------------
        [Fact]
        public void Compare_SortsCorrectly()
        {
            var org1 = new Organization("Org1", 10);
            var org2 = new Organization("Org2", 20);
            var org3 = new Organization("Org3", 15);

            var comparer = new OrganizationByEmployeesComparer();

            // org1 < org2
            Assert.True(comparer.Compare(org1, org2) < 0);

            // org2 > org1
            Assert.True(comparer.Compare(org2, org1) > 0);

            // org1 < org3
            Assert.True(comparer.Compare(org1, org3) < 0);

            // org2 > org3
            Assert.True(comparer.Compare(org2, org3) > 0);

            // org1 == org1
            Assert.Equal(0, comparer.Compare(org1, org1));
        }

        [Fact]
        public void Compare_InvalidObjects_Throws()
        {
            var comparer = new OrganizationByEmployeesComparer();
            var org = new Organization("Org", 10);

            Assert.Throws<ArgumentException>(() => comparer.Compare(org, "string"));
            Assert.Throws<ArgumentException>(() => comparer.Compare(5, org));
            Assert.Throws<ArgumentException>(() => comparer.Compare("a", "b"));
        }

        [Fact]
        public void Compare_NullObjects_Throws()
        {
            var comparer = new OrganizationByEmployeesComparer();
            Organization nullOrg = null;

            Assert.Throws<ArgumentException>(() => comparer.Compare(nullOrg, new Organization()));
            Assert.Throws<ArgumentException>(() => comparer.Compare(new Organization(), nullOrg));
            Assert.Throws<ArgumentException>(() => comparer.Compare(nullOrg, nullOrg));
        }
    }
}
