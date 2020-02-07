<template>
  <nav id="sidebar">
    <div class="sidebar-header d-flex justify-content-center">
      <router-link to="/">
        <img id="logo" src="/logo.svg" class="logo" />
      </router-link>
    </div>

    <ul v-if="!admin" class="list-unstyled components">
      <li class="active">
        <router-link class="nav-item" to="/dashboard">
          <img src="/icon/home.svg" class="icon" />
          <span class="hide">Home</span>
        </router-link>
      </li>
      <li>
        <router-link class="nav-item" to="/dashboard/animais">
          <img src="/icon/animals.svg" class="icon" />
          <span class="hide">Animais</span>
        </router-link>
      </li>
      <li>
        <router-link class="nav-item" to="/dashboard/eventos">
          <img src="/icon/events.svg" class="icon" />
          <span class="hide">Eventos</span>
        </router-link>
      </li>
      <li>
        <router-link class="nav-item" to="/dashboard/novidades">
          <img src="/icon/news.svg" class="icon" />
          <span class="hide">Novidades</span>
        </router-link>
      </li>
    </ul>

    <ul v-if="admin" class="list-unstyled components">
      <li class="active">
        <router-link class="nav-item" to="/dashboard/users">
          <img src="/icon/users.svg" class="icon" />
          <span class="hide">Utilizadores</span>
        </router-link>
      </li>
    </ul>

    <img
      id="sidebarCollapse"
      src="/icon/arrow-left.svg"
      class="collapseImage"
      @click="collapseSidebar()"
    />
  </nav>
</template>

<style scoped>
#sidebar {
  width: 250px;
  min-height: 100vh;
  height: 100%;
  z-index: 1;
  background: #25348a;
  color: #fff;
  transition: all 0.3s;
  position: relative;
}

#sidebar.active {
  width: 70px;
}

a,
a:hover,
a:focus {
  color: inherit;
  text-decoration: none;
  transition: all 0.3s;
}

.sidebar-header {
  padding: 20px;
}

#sidebar ul li a {
  padding: 10px;
  font-size: 1.1em;
}

#sidebar ul li:hover {
  background: rgb(75, 97, 184);
}

ul ul a {
  font-size: 0.9em !important;
  padding-left: 30px !important;
  background: #6d7fcc;
}
.logo {
  max-width: 100px;
}
.logo.active {
  max-width: 40px;
}

.icon {
  width: 25px;
  height: 25px;
  margin: 5%;
  margin-right: 5px;
}
.icon.active {
  margin: 20% auto;
  display: block;
  cursor: pointer;
}

.collapseImage {
  width: 40px;
  position: absolute;
  bottom: 1%;
  left: 50%;
  transform: translate(-50%, 0);
  z-index: 1;
  cursor: pointer;
}
@media (min-width: 768px) {
  #sidebar ul li a {
    display: inline;
  }
}
@media (max-width: 768px) {
  #sidebar {
    width: 50px;
  }
  #sidebar.active {
    width: 50px;
  }
  .hide {
    display: none;
  }
  .logo {
    max-width: 40px;
  }
  .icon {
    width: 25px;
    height: 25px;
    margin: 10% auto;
    display: block;
    cursor: pointer;
  }
}
</style>


<script>
import $ from "jquery";
export default {
  name: "sidebar",
  data() {
    return {
      collapsed: null
    };
  },
  props: ["admin"],
  mounted() {
    var $window = $(window);
    var windowsize = $window.width();
    if (windowsize < 768) this.collapsed = true;
    else this.collapsed = false;
  },
  methods: {
    collapseSidebar() {
      $("#sidebar").toggleClass("active");
      $(".logo").toggleClass("active");
      $(".icon").toggleClass("active");
      if (this.collapsed == false) {
        $(".hide").css("display", "none");
        $(".collapseImage").attr("src", "/icon/arrow-right-white.svg");
        this.collapsed = true;
      } else {
        $(".hide").css("display", "inline");
        $(".collapseImage").attr("src", "/icon/arrow-left.svg");
        this.collapsed = false;
      }
    }
  }
};
</script>