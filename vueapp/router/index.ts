import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../src/views/Home.vue'
import AddEvent from '../src/views/AddEvent.vue'
import EventDetailsView from '../src/views/EventDetailsView.vue'

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
    },
    {
        path: '/eventDetails/:eventId',
        name: 'eventDetailsView',
        component: EventDetailsView
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
