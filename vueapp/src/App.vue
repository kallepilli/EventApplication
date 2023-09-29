<template>
    <div>
        <nav class="navbar navbar-expand-lg navbar-light bg-light fixed-top">
            <div class="container">
                <router-link to="/" class="navbar-brand">
                    <img src="../src/assets/logo.png" alt="Event App" class="logo-img" style="">
                </router-link>
                <div class="container">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <router-link to="/" class="nav-link">Avaleht</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/addEvent" class="nav-link">Lisa &uuml;ritus</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </div>
    <div class="container fixed-top" style="margin-top: 80px;">
        <router-view />
    </div>
</template>

<script>
    export default {
        // Your script setup goes here
    };
</script>

<style scoped>
    /* Additional styles for the navbar */
    .navbar {
        border-bottom: 1px solid #e0e0e0;
    }

    .navbar-brand {
        font-weight: bold;
    }

    .logo-img {
        max-width: 120px; /* Adjust the size as needed */
    }

    /* Add any additional styling here if needed */
</style>
