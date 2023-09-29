<template>
    <div style="max-width:500px;">
        <h3>Lisa uus &uuml;ritus</h3>
        <form @submit.prevent="createEvent">
            <div class="mb-3">
                <label for="eventName" class="form-label">&Uuml;rituse nimetus</label>
                <input v-model="event.Name" type="text" class="form-control" id="eventName" required>
            </div>
            <div class="mb-3">
                <label for="eventDate" class="form-label">Toimumise aeg</label>
                <input v-model="event.EventTime" type="date" class="form-control" id="eventDate" required>
            </div>
            <div class="mb-3">
                <label for="eventLocation" class="form-label">Asukoht</label>
                <input v-model="event.Location" type="text" class="form-control" id="eventLocation" required>
            </div>
            <div class="mb-3">
                <label for="additionalInfo" class="form-label">Lisainfo</label>
                <textarea v-model="event.AdditionalInfo" maxlength="1000" class="form-control" id="additionalInfo"></textarea>
            </div>
            <button :disabled="!isFilled()" type="submit" @click="submit()" class="btn btn-primary">Loo uus &uuml;ritus</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import { Ref, ref } from 'vue';
import type { EventDTO } from '../model/EventDTO';


    const event: Ref<EventDTO> = ref({
    Name: '',
    EventTime: undefined,
    Location: '',
    AdditionalInfo: ''
    });

    const submit = async () => {
        try {
            const response = await fetch('https://localhost:7165/event', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(event.value),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            event.value = data;

            event.value.Name = '';
            event.value.EventTime = new Date();
            event.value.Location = '';
            event.value.AdditionalInfo = '';

            console.log('Event created successfully:', data);
        } catch (error) {
            console.error('Error creating event:', error);
        }

    };

    const isFilled = () => {
        if (event.value.Name == '' || event.value.Location == '' || event.value.EventTime == undefined ) {
            return false;
        }
        return true;
    };



console.log(event)


</script>
