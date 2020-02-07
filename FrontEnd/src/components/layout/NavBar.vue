<template>
  <div>
    <nav id="navbar" class="navbar fixed-top navbar-expand-lg">
      <a class="navbar-brand" href="/">
        <router-link to="/" class="d-flex align-items-center">
          <img src="/logo.svg" class="d-inline-block align-top icon" alt />
          <span class="app-name">PetCare</span>
        </router-link>
      </a>

      <div class="navbar-collapse collapse w-100 order-3 dual-collapse2">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item">
            <router-link class="nav-link" to="/parceiros">Parceiros</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/eventos">Eventos</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/perdidos">Perdidos</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/amiguinhos">Amiguinhos</router-link>
          </li>
          <li v-if="!logged" class="nav-item">
            <span class="spmodal nav-link" data-target="#loginModal" data-toggle="modal">Login</span>
          </li>
          <li v-if="logged" class="nav-item">
            <router-link class="nav-link" to="/dashboard">Dashboard</router-link>
          </li>
          <li v-if="logged" class="nav-item">
            <span class="spmodal nav-link" @click="logout()">Logout</span>
          </li>
        </ul>
      </div>
      <div class="dropdown show order-3">
        <a
          class="btn"
          role="button"
          id="dropdownMenu"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
        >
          <img src="/icon/menu.svg" class="icon" />
        </a>

        <div class="dropdown-menu dropdown-menu-right">
          <a class="dropdown-item" href="/parceiros">Parceiros</a>
          <a class="dropdown-item" href="/eventos">Eventos</a>
          <a class="dropdown-item" href="/perdidos">Perdidos</a>
          <a class="dropdown-item" href="/amiguinhos">Amiguinhos</a>
          <span class="dropdown-item spmodal" data-target="#loginModal" data-toggle="modal">Login</span>
        </div>
      </div>
    </nav>
    <Login></Login>
  </div>
</template>



<script>
import Login from "../auth/Login.vue";
import JQuery from "jquery";
let $ = JQuery;

export default {
  name: "NavBar",
  data() {
    return {
      logged: null
    };
  },
  components: {
    Login
  },
  created() {
    this.logged = localStorage.token;
    window.addEventListener("scroll", this.handleScroll);
  },
  destroyed() {
    window.removeEventListener("scroll", this.handleScroll);
  },
  methods: {
    handleScroll(e) {
      if (e.target.documentElement.scrollTop >= 300) {
        $("#navbar").css("background", "rgba(0, 0, 0, 0.335)");
      } else {
        $("#navbar").css("background", "transparent");
      }
    },
    logout() {
      localStorage.removeItem("token");
      location.reload();
    }
  }
};
</script>

<style scoped>
.navbar {
  transition: 0.2s ease;
}

.navbar-brand .app-name {
  color: white;
  text-decoration: none;
  font-size: 1.2em;
}

.nav-link {
  color: white;
  font-size: 1.2em;
  cursor: pointer;
}

.icon {
  width: 30px;
  height: 30px;
}

@media only screen and (min-width: 992px) {
  .dropdown {
    display: none;
  }
}
</style>