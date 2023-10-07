import { createApp } from 'vue';
import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.css';
import './assets/style.css';
import App from './App.vue';
import router from '../router/index';

const app = createApp(App);
app.use(router);

app.mount('#app')