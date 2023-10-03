import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../src/views/Home.vue';
import AddEvent from '../src/views/AddEvent.vue';
import EventDetailsView from '../src/views/EventDetailsView.vue';
import ChangeParticipantView from '../src/views/ChangeParticipantView.vue';
import ChangeEventView from '../src/views/ChangeEventView.vue';

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
        path: '/changeParticipant/:participantId',
        name: 'changeParticipant',
        component: ChangeParticipantView
    },
    {
        path: '/changeEvent/:eventId',
        name: 'changeEvent',
        component: ChangeEventView
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
