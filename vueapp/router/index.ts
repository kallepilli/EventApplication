import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../src/views/Home.vue'
import AddEvent from '../src/views/AddEvent.vue'


const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'home',
        component: Home,
    },
    {
        path: '/addEvent',
        name: 'addEvent',
        component: AddEvent,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
