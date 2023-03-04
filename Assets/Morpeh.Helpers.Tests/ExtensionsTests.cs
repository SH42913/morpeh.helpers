namespace Morpeh.Helpers.Tests {
    using NUnit.Framework;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Helpers;

    public class ExtensionsTests : EcsTestFixture {
        private struct Test : IComponent {
            public int test;
        }

        private Stash<Test> testStash;

        protected override void InitSystems(SystemsGroup systemsGroup) { }

        [SetUp]
        public void Prepare() {
            testStash = testWorld.GetStash<Test>();
        }

        [Test]
        public void StashAddOrGet_Add() {
            Entity entity = testWorld.CreateEntity();

            ref Test test = ref testStash.AddOrGet(entity);
            Assert.True(testStash.Has(entity));

            CheckRefIsReal(entity, ref test);
        }

        [Test]
        public void StashAddOrGet_Get() {
            Entity entity = testWorld.CreateEntity();
            entity.SetComponent(new Test());

            ref Test test = ref testStash.AddOrGet(entity);
            CheckRefIsReal(entity, ref test);
        }

        [Test]
        public void EntityAddOrGet_Add() {
            Entity entity = testWorld.CreateEntity();

            ref Test test = ref entity.AddOrGet<Test>();
            Assert.True(testStash.Has(entity));

            CheckRefIsReal(entity, ref test);
        }

        [Test]
        public void EntityAddOrGet_Get() {
            Entity entity = testWorld.CreateEntity();
            entity.SetComponent(new Test());

            ref Test test = ref entity.AddOrGet<Test>();
            CheckRefIsReal(entity, ref test);
        }

        private void CheckRefIsReal(Entity entity, ref Test test) {
            const int newValue = 5;
            test.test = newValue;
            Assert.AreEqual(newValue, testStash.Get(entity).test, "Ref is not real!");
        }
    }
}