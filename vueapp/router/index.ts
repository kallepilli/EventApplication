import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../src/views/Home.vue';
import AddEvent from '../src/views/AddEvent.vue';
import EventDetailsView from '../src/views/EventDetailsView.vue';
import UpdateParticipantView from '../src/views/UpdateParticipantView.vue';
import UpdateEventView from '../src/views/UpdateEventView.vue';

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
    },
    {
        path: '/updateParticipant/:eventParticipantId',
        name: 'updateParticipant',
        component: UpdateParticipantView
    },
    {
        path: '/updateEvent/:eventId',
        name: 'updateEvent',
        component: UpdateEventView
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
