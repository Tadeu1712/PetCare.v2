<template>
  <div id="app">
    <NavBar v-if="!session" />
    <router-view />
    <FPFooter v-if="!session" />
  </div>
</template>

<style>
#app {
  background: #fafafa;
}
</style>


<script>
import NavBar from "./components/layout/NavBar.vue";
import FPFooter from "./components/layout/FPFooter.vue";

export default {
  name: "app",
  components: {
    NavBar,
    FPFooter
  },
  data() {
    return {
      session: false,
      scrollPosition: 0
    };
  },
  watch: {
    $route: {
      handler: "currentRoute",
      immediate: true
    }
  },
  methods: {
    currentRoute() {
      this.$nextTick(() => {
        if (this.$route.name.includes("dashboard")) {
          this.session = true;
        } else {
          this.session = false;
        }
      });
    }
  }
};
</script>