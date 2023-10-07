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
        <div class="scrollable-content">
            <router-view />
        </div>
    </div>
</template>

<script setup lang="ts">

</script>