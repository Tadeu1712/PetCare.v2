import Vue from 'vue'
import Router from 'vue-router'
import Homepage from "./components/Homepage.vue";
import Login from './components/auth/Login.vue';
import Events from './components/Events.vue';
import Partners from './components/Partners.vue';
import Partner from './components/Partner.vue';
import Lost from './components/Lost.vue';
import Kids from './components/Kids.vue';
import Dashboard from './components/dashboard/Dashboard.vue';
import AnimalsDashboard from './components/dashboard/Animals.vue';
import EventsDashboard from './components/dashboard/Events.vue';
import NewsDashboard from './components/dashboard/News.vue';
import Users from './components/dashboard/Users.vue';
import Settings from './components/dashboard/Settings.vue';
import Teste from './components/Teste.vue';

Vue.use(Router)

const routes = [
    { path: '/teste', component: Teste, name: "teste" },
    { path: '/', component: Homepage, name: "homepage" },
    { path: '/parceiros', component: Partners, name: "partners" },
    { path: '/eventos', component: Events, name: "events" },
    { path: '/login', component: Login, name: "login" },
    { path: '/perdidos', component: Lost, name: "lost" },
    { path: '/amiguinhos', component: Kids, name: "kids" },
    { path: '/parceiro/:id', component: Partner, name: "partner" },
    {
        path: '/dashboard', component: Dashboard, children: [
            {
                path: '',
                name: "dashboard",
                component: Settings
            },
            {
                path: 'animais',
                name: "dashboard animals",
                component: AnimalsDashboard
            },
            {
                path: 'eventos',
                name: "dashboard events",
                component: EventsDashboard
            },
            {
                path: 'novidades',
                name: "dashboard news",
                component: NewsDashboard
            },
            {
                path: 'users',
                name: "dashboard users admin",
                component: Users
            }
        ]
    },
];



export default routes;