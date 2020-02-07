<template>
  <div>
    <Header
      title="Eventos"
      description="Eventos do seu interesse"
      image="background-green.svg"
      class="mb-5"
    ></Header>
    <div class="container h-100 my-3">
      <div class="d-flex justify-content-center">
        <SearchBar :search="search" />
      </div>
    </div>
    <div class="container margin">
      <div class="row">
        <div class="col-sm-6 event-search" v-for="event in events" v-bind:key="event.id">
          <div class="card my-5">
            <div class="img-container">
              <img class="card-img-top" v-bind:src="event.image" alt="Card image cap" />
              <div class="event-date-container">
                <h2
                  class="event-date"
                >{{new Date(event.dateInit).toLocaleDateString("pt-PT", options)}}</h2>

                <p class="event-date-from-to">
                  <img src="/icon/hour.svg" class="icon" />
                  {{new Date(event.dateInit).toLocaleDateString()}} - {{new Date(event.dateEnd).toLocaleDateString()}}
                </p>
              </div>
            </div>

            <div class="card-body">
              <h5 class="card-title">{{event.title}}</h5>
              <p class="card-text">
                <img src="/icon/address.svg" class="icon" />
                {{event.location}}
              </p>
              <p class="card-text">{{event.description}}</p>
            </div>

            <div class="card-footer d-flex justify-content-end align-items-center">
              <div class="d-flex align-items-center">
                <img src="/icon/euro.svg" class="icon" />
                <span class="price-tag">{{event.price > 0 ? event.price + '€' : "Grátis" }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Header from "./layout/Header.vue";
import SearchBar from "./layout/SearchBar.vue";

const { url, external_url } = require("../../helper");

export default {
  name: "events",
  components: {
    Header,
    SearchBar
  },
  data() {
    return {
      events: null,
      url: external_url,
      search: ".event-search",
      options: {
        year: "numeric",
        month: "long",
        day: "numeric"
      }
    };
  },
  created: function() {
    axios.get(url + "/events/all").then(res => {
      this.events = res.data.data;
    });
  }
};
</script>

<style scoped>
.img-container {
  position: relative;
}

.icon {
  width: 18px;
  height: 18px;
}

.price-icon {
  width: 30px;
  height: 30px;
}

.event-date-container {
  position: absolute;
  background-color: rgba(184, 233, 255, 0.835);
  color: rgb(1, 1, 87);
  top: 50%;
  transform: translateY(-50%);
  left: -10px;
  padding: 15px 60px 15px 30px;
}

.price-tag {
  color: rgb(1, 1, 87);
  font-size: 1.5em;
  font-weight: bold;
}

.card-footer {
  background: white;
  border: none;
}

@media only screen and (max-width: 768px) {
  .price-tag {
    font-size: 1em;
  }
  .card-text {
    font-size: 0.8em;
  }
  .event-date,
  .card-title {
    font-size: 1.2em;
  }
  .icon {
    width: 13px;
    height: 13px;
  }
  .event-date-from-to {
    font-size: 0.7em;
  }
  .event-date-container {
    padding: 10px 20px 10px 30px;
  }
}
@media only screen and (max-width: 560px) {
  .price-tag,
  .event-date-from-to {
    font-size: 1.3em;
  }
  .event-date {
    font-size: 1.7em;
  }
  .event-date-container {
    padding: 15px 60px 15px 30px;
  }
  .card-text {
    font-size: 0.9em;
  }
  .icon {
    width: 18px;
    height: 18px;
  }
}

@media only screen and (max-width: 450px) {
  .price-tag {
    font-size: 1em;
  }
  .card-text {
    font-size: 0.8em;
  }
  .event-date {
    font-size: 1.3em;
  }
  .event-date-from-to {
    font-size: 0.7em;
  }
  .icon {
    width: 13px;
    height: 13px;
  }
  .event-date-container {
    padding: 10px 15px 10px 30px;
  }
}
</style>
