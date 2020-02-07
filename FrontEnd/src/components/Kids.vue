<template>
  <div class="background">
    <div class="kids-header">
      <Header size="500" image="background-kids.png"></Header>
      <div id="kids-carousel" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
          <div class="carousel-item active">
            <img class="d-block w-100" src="/kids/carousel1.jpg" alt="First slide" />
          </div>
          <div class="carousel-item">
            <img class="d-block w-100" src="/kids/carousel2.jpg" alt="Second slide" />
          </div>
          <div class="carousel-item">
            <img class="d-block w-100" src="/kids/carousel3.webp" alt="Third slide" />
          </div>
        </div>
        <a class="carousel-control-prev" href="#kids-carousel" role="button" data-slide="prev">
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#kids-carousel" role="button" data-slide="next">
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="sr-only">Next</span>
        </a>
      </div>
    </div>

    <div class="container margin">
      <div v-for="(animal, index) in animals" v-bind:key="animal.id" class="scroll-animations">
        <div v-if="index % 2 != 0" class="row animated-left animated hidden">
          <div class="col-lg-8">
            <img v-if="animal.image" v-bind:src="animal.image" class="animal-img animal-left" />
            <img v-else src="/default-animal.jpg" class="card-img-top" />
          </div>
          <div class="animal-info col-lg-4">
            <h5 class="card-title">{{animal.name}}</h5>
            <div class="progress-container">
              <div class="progress-item">
                <h3 class="progress-title">Engraçado</h3>
                <div class="progress">
                  <div
                    class="progress-bar progress-bar-animated progress-bar-striped bg-success"
                    role="progressbar"
                    v-bind:style="{ width: animal.funny + '%'}"
                    aria-valuenow="25"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  ></div>
                </div>
              </div>

              <div class="progress-item">
                <h3 class="progress-title">Energia</h3>
                <div class="progress">
                  <div
                    class="progress-bar progress-bar-animated progress-bar-striped bg-info"
                    role="progressbar"
                    v-bind:style="{ width: animal.energy + '%'}"
                    aria-valuenow="75"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  ></div>
                </div>
              </div>

              <div class="progress-item">
                <h3 class="progress-title">Calmo</h3>
                <div class="progress">
                  <div
                    class="progress-bar progress-bar-animated progress-bar-striped bg-warning"
                    role="progressbar"
                    v-bind:style="{ width: animal.chill + '%'}"
                    aria-valuenow="25"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  ></div>
                </div>
              </div>

              <div class="progress-item">
                <h3 class="progress-title">Travesso</h3>
                <div class="progress">
                  <div
                    class="progress-bar progress-bar-animated progress-bar-striped bg-danger"
                    role="progressbar"
                    v-bind:style="{ width: animal.troubleMaker + '%'}"
                    aria-valuenow="25"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div v-if="index % 2 == 0" class="row animated-right animated hidden">
          <div class="order-lg-2 col-lg-8 order-xl-2">
            <img v-bind:src="animal.image" class="animal-img animal-right" />
          </div>
          <div class="animal-info order-lg-1 col-lg-4 order-xl-1">
            <h5 class="card-title">{{animal.name}}</h5>
            <div class="progress-item">
              <h3 class="progress-title">Engraçado</h3>
              <div class="progress">
                <div
                  class="progress-bar progress-bar-animated progress-bar-striped bg-success"
                  role="progressbar"
                  v-bind:style="{ width: animal.funny + '%'}"
                  aria-valuenow="25"
                  aria-valuemin="0"
                  aria-valuemax="100"
                ></div>
              </div>
            </div>

            <div class="progress-item">
              <h3 class="progress-title">Energia</h3>
              <div class="progress">
                <div
                  class="progress-bar progress-bar-animated progress-bar-striped bg-info"
                  role="progressbar"
                  v-bind:style="{ width: animal.energy + '%'}"
                  aria-valuenow="25"
                  aria-valuemin="0"
                  aria-valuemax="100"
                ></div>
              </div>
            </div>

            <div class="progress-item">
              <h3 class="progress-title">Calmo</h3>
              <div class="progress">
                <div
                  class="progress-bar progress-bar-animated progress-bar-striped bg-warning"
                  role="progressbar"
                  v-bind:style="{ width: animal.chill + '%'}"
                  aria-valuenow="25"
                  aria-valuemin="0"
                  aria-valuemax="100"
                ></div>
              </div>
            </div>

            <div class="progress-item">
              <h3 class="progress-title">Travesso</h3>
              <div class="progress">
                <div
                  class="progress-bar progress-bar-animated progress-bar-striped bg-danger"
                  role="progressbar"
                  v-bind:style="{ width: animal.troubleMaker + '%'}"
                  aria-valuenow="25"
                  aria-valuemin="0"
                  aria-valuemax="100"
                ></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="bottom-background"></div>
  </div>
</template>

<script>
import axios from "axios";
import Header from "./layout/Header.vue";
import JQuery from "jquery";
let $ = JQuery;
const { url, external_url } = require("../../helper");

export default {
  name: "homepage",
  components: {
    Header
  },
  data() {
    return {
      animals: null,
      url: external_url
    };
  },
  computed: {
    width() {
      return 50;
    }
  },
  mounted() {
    const vm = this;

    $(window).scroll(function() {
      $(".animated").removeClass("hidden");
      $(".scroll-animations .animated-left").each(function() {
        if (vm.isScrolledIntoView(this) === true) {
          $(this).addClass("bounceInLeft");
        }
      });
      $(".scroll-animations .animated-right").each(function() {
        if (vm.isScrolledIntoView(this) === true) {
          $(this).addClass("bounceInRight");
        }
      });
    });
  },
  created: function() {
    axios.get(url + "/animal/babies").then(res => {
      this.animals = res.data.data;
    });
  },
  methods: {
    isScrolledIntoView(elem) {
      var docViewTop = $(window).scrollTop();
      var docViewBottom = docViewTop + $(window).height();

      var elemTop = $(elem).offset().top;
      var elemBottom = elemTop + $(elem).height();

      return elemBottom < docViewBottom + 500 && elemTop > docViewTop - 500;
    }
  }
};
</script>

<style scoped>
@import "/css/animate.css";
.hidden {
  visibility: hidden;
}
.kids-header {
  position: relative;
  margin-bottom: 3%;
}
.carousel {
  @apply --container;
  max-width: 800px;
  position: absolute;
  top: 180px;
  left: 0;
  right: 0;
  margin: auto;
}
.container {
  padding-top: 5%;
}
.row {
  margin: 10% auto;
}

.animal-info {
  display: flex;
  justify-content: center;
  flex-direction: column;
  font-family: "Courgette", cursive;
  text-align: center;
}

.card-title {
  font-size: 2.5em;
  font-weight: bold;
  margin-bottom: 5%;
}

.progress-title {
  text-align: left;
  font-size: 1.3em;
}

.nav-link {
  color: white;
}

.progress {
  height: 20px;
}

.animal-img {
  width: 100%;
  border-radius: 10px;
  border: 5px outset rgba(0, 204, 255, 0.452);
}

.animal-left {
  border-radius: 0px 40px 0px 40px;
}

.animal-right {
  border-radius: 40px 0px 40px 0px;
}

.bottom-background {
  height: 400px;
  background-image: url("https://i.ya-webdesign.com/images/grass-clipart-mountain-1.png");
  background-size: 100% 100%;
  background-repeat: no-repeat;
  background-position: center bottom;
}

.background {
  background: linear-gradient(
    top,
    rgba(255, 255, 255) 0%,
    rgb(255, 237, 237) 100%
  );
}

@media only screen and (max-width: 850px) {
  .carousel {
    width: 95%;
    top: 150px;
  }
  .kids-header {
    margin-bottom: 15%;
  }
}

@media only screen and (max-width: 576px) {
  .carousel {
    top: 200px;
  }
}

@media only screen and (max-width: 450px) {
  .carousel {
    top: 230px;
  }
}
</style>