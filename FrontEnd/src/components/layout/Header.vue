<template>
  <div :style="style" class="jumbotron jumbotron-fluid homepage">
    <div class="container">
      <h1 class="display-4">{{title}}</h1>
      <p class="lead">{{description}}</p>
    </div>
    <img v-if="logo" v-bind:src="logo" class="logo rounded" />
  </div>
</template>

<script>
import { sizes } from "../../../helper.js";
export default {
  name: "Header",
  computed: {
    style() {
      if (this.size) {
        return (
          "background-image: url(/background/" +
          this.image +
          "); height: " +
          this.size +
          "px;"
        );
      }
      return "background-image: url(/background/" + this.image + ")";
    }
  },
  props: ["title", "description", "image", "logo", "size"]
};
</script>

<style scoped>
.jumbotron {
  height: 350px;
  background-image: url("/background/{{image}}");
  background-attachment: fixed;
  background-size: cover;
  box-shadow: 0px 0px 10px rgb(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  margin: 0;
  position: relative;
}

.logo {
  position: absolute;
  left: 0;
  right: 0;
  margin-left: auto;
  margin-right: auto;
  width: 250px;
  margin-bottom: -200px;
  box-shadow: 0px 0px 10px rgb(0, 0, 0, 0.5);
}

.display-4,
.lead {
  color: white;
  text-align: center;
}

.display-4 {
  font-weight: bold;
  text-transform: uppercase;
  font-size: 5rem;
}

@media only screen and (max-width: 992px) {
  .display-4 {
    font-size: 3rem;
  }
}

@media only screen and (max-width: 768px) {
  .jumbotron {
    max-height: 350px;
  }
}

@media only screen and (max-width: 576px) {
  .display-4 {
    font-size: 2rem;
  }
}
</style>