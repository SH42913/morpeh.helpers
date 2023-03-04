namespace Morpeh.Helpers.Tests {
    using System.Collections;
    using NUnit.Framework;
    using Scellecs.Morpeh;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.TestTools;

    [TestFixture]
    public abstract class EcsTestFixture {
        protected World testWorld;

        private SystemsGroup testSystems;

        [SetUp]
        public void FixtureSetUp() {
            testWorld = World.Create();
            testWorld.UpdateByUnity = false;

            testSystems = testWorld.CreateSystemsGroup();
            InitSystems(testSystems);
            testWorld.AddSystemsGroup(0, testSystems);
            testWorld.Update(0f);
        }

        [TearDown]
        public void FixtureTearDown() {
            testSystems.Dispose();
            testSystems = null;

            testWorld.Dispose();
            testWorld = null;
        }

        [UnityTearDown]
        public IEnumerator SceneTearDown() {
            Scene scene = SceneManager.GetActiveScene();
            foreach (GameObject o in scene.GetRootGameObjects()) {
                if (o.name.EndsWith("tests runner")) {
                    continue;
                }

                Object.Destroy(o);
            }

            yield return null;
        }

        protected abstract void InitSystems(SystemsGroup systemsGroup);

        protected void RegisterAdditionalSystems(ISystem[] systems) {
            SystemsGroup systemsGroup = testWorld.CreateSystemsGroup();
            foreach (ISystem system in systems) {
                systemsGroup.AddSystem(system);
            }

            testWorld.AddSystemsGroup(1, systemsGroup);
            testWorld.Update(0f);
        }

        protected void RunFixedSystems() {
            RefreshFilters();
            testWorld.FixedUpdate(Time.fixedDeltaTime);
            RefreshFilters();
        }

        protected void RunUpdateSystems(float dt) {
            RefreshFilters();
            testWorld.Update(dt);
            RefreshFilters();
        }

        protected void RunLateUpdateSystems(float dt) {
            RefreshFilters();
            testWorld.LateUpdate(dt);
            RefreshFilters();
        }

        protected void RefreshFilters() {
            testWorld.Commit();
        }
    }
}