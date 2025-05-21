namespace Morpeh.Helpers.Tests {
    using NUnit.Framework;
    using Scellecs.Morpeh;
    using Scellecs.Morpeh.Helpers;
    using Scellecs.Morpeh.Helpers.OneFrame;

    public class ExtensionsTests : EcsTestFixture {
        private struct Test : IComponent {
            public int test;
        }

        private Stash<Test> testStash;
        private Filter testFilter;

        protected override void InitSystems(SystemsGroup systemsGroup) { }

        public override void FixtureSetUp() {
            OneFramePlugin.Init();
            base.FixtureSetUp();
        }

        [SetUp]
        public void Prepare() {
            testStash = testWorld.GetStash<Test>();
            testFilter = testWorld.Filter.With<Test>().Build();
        }

        [Test]
        public void FilterRemoveComponentsForAll() {
            const int count = 10;
            for (var i = 0; i < count; i++) {
                CreateEntityWithTest();
            }

            RefreshFilters();
            Assert.AreEqual(count, testFilter.GetLengthSlow());

            testFilter.RemoveComponentForAll<Test>();
            RefreshFilters();

            Assert.AreEqual(0, testFilter.GetLengthSlow());
        }

        [Test]
        public void FilterRemoveAllEntities() {
            Entity first = CreateEntityWithTest();
            Entity second = CreateEntityWithTest();

            RefreshFilters();
            Assert.AreEqual(2, testFilter.GetLengthSlow());

            testFilter.RemoveAllEntities();
            RefreshFilters();

            Assert.False(first.Exists());
            Assert.False(second.Exists());
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
            Entity entity = CreateEntityWithTest();

            ref Test test = ref testStash.AddOrGet(entity);

            CheckRefIsReal(entity, ref test);
        }

        [Test]
        public void OneFrame_Clean() {
            testWorld.RegisterOneFrame<Test>();
            CreateEntityWithTest();

            testWorld.CleanupUpdate(0f);
            RefreshFilters();

            Assert.That(testFilter.IsEmpty);
        }

        private Entity CreateEntityWithTest() {
            Entity entity = testWorld.CreateEntity();
            testStash.Add(entity);
            return entity;
        }

        private void CheckRefIsReal(Entity entity, ref Test test) {
            const int newValue = 5;
            test.test = newValue;
            Assert.AreEqual(newValue, testStash.Get(entity).test, "Ref is not real!");
        }
    }
}