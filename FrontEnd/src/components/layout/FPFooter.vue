<template functional>
  <footer class="page-footer special-color-dark pt-4">
    <div class="container">
      <div class="footer-top">
        <div class="footer-about">
          <h2>Sobre</h2>
          <p>Plataforma que permite aos seus utentes obter informações de várias associações num só lugar. O que irá beneficiar as associações no sentido em que será mais fácil de divulgar campanhas de adoção, vacinação e esterilização.</p>
        </div>
        <div class="footer-links">
          <h2>Links</h2>

          <router-link class="footer-link" to="/parceiros">Parceiros</router-link>
          <router-link class="footer-link" to="/eventos">Eventos</router-link>
          <router-link class="footer-link" to="/perdidos">Perdidos</router-link>
          <router-link class="footer-link" to="/amiguinhos">Amiguinhos</router-link>
        </div>
      </div>
      <div class="footer-down">
        <div
          class="footer-copyright text-center py-3 my-3"
        >Copyright © 2020 All Rights Reserved by PetCare</div>
      </div>
    </div>
  </footer>
</template>

<style scoped>
.page-footer {
  min-height: 250px;
  background: rgb(101, 101, 238);
  color: white;
  border-top: 20px solid rgb(68, 68, 218);
}

.container {
  display: flex;
  flex-direction: column;
}

.footer-links {
  min-width: 200px;
}

.footer-about {
  width: 50%;
}

.footer-link {
  margin: 0;
  display: block;
  color: white;
}

.footer-link:hover {
  text-decoration: none;
  font-weight: bold;
  color: white;
}

.footer-top {
  display: flex;
  justify-content: space-between;
  margin: auto;
  flex-wrap: wrap;
  width: 80%;
}

@media only screen and (max-width: 768px) {
  .footer-top {
    width: 100%;
  }

  .footer-about {
    width: 100%;
    text-align: center;
  }

  .footer-links {
    width: 100%;
    text-align: center;
  }
}
</style>