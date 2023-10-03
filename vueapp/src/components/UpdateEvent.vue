<template>
    <div style="max-width:500px;">
        <h3>Muuda &uuml;ritust</h3>
        <form @submit.prevent="createEvent">
            <div class="mb-3">
                <label for="eventName" class="form-label">&Uuml;rituse nimetus</label>
                <input v-model="event.Name" type="text" class="form-control" id="eventName" required>
            </div>
            <div class="mb-3">
                <label for="eventDate" class="form-label">Toimumise aeg</label>
                <input v-model="event.EventTime" :min="todayDate" type="date" class="form-control" id="eventDate" required>
            </div>
            <div class="mb-3">
                <label for="eventLocation" class="form-label">Asukoht</label>
                <input v-model="event.Location" type="text" class="form-control" id="eventLocation" required>
            </div>
            <div class="mb-3">
                <label class="form-label" for="additionalInfo">Lisainfo ({{ charactersLeft }} / {{ additionalInfoMaxlength }})</label>
                <textarea v-model="event.AdditionalInfo"
                          :maxlength="additionalInfoMaxlength"
                          class="form-control"
                          id="additionalInfo"
                          :class="{ 'is-invalid': characterLimitExceeded }"></textarea>
                <div v-if="characterLimitExceeded" class="text-danger">
                    Lisainfo &uuml;letab lubatud arvu {{ charactersLeft * -1 }} v&otilde;rra!
                </div>
            </div>
            <button type="button" @click="toHomepage()" class="btn btn-secondary" style="margin-right:3px;">Tagasi</button>
            <button :disabled="!isFilled() || characterLimitExceeded" type="submit" @click="submit()" class="btn btn-primary">Muuda &uuml;ritust</button>
        </form>
    </div>
</template>

<script setup lang="ts">
    import { Ref, ref, computed, onMounted } from 'vue';
    import type { EventDTO } from '../model/EventDTO';
    import { useRoute, useRouter } from 'vue-router';

    const router = useRouter();
    const route = useRoute();
    const event: Ref<EventDTO> = ref({
        Name: '',
        EventTime: undefined,
        Location: '',
        AdditionalInfo: ''
    });

    const todayDate = new Date().toISOString().split('T')[0];
    const eventId = route.params.eventId;

    const fetchEvent = async () => {
        const eventResponse = ref([]);
        try {
            const response = await fetch('https://localhost:7165/event/' + eventId);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            eventResponse.value = data;
        } catch (error) {
            console.error('Error fetching participant', error);
        }
        event.value.Name = eventResponse.value.name;
        event.value.AdditionalInfo = eventResponse.value.additionalInfo;
        event.value.EventTime = formatDate(eventResponse.value.eventTime);
        event.value.Location = eventResponse.value.location;
    };

    const submit = async () => {
        try {
            const response = await fetch('https://localhost:7165/event/' + eventId, {
                method: 'PUT',
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
        router.push('/');
    };

    const isFilled = () => {
        if (event.value.Name == '' || event.value.Location == '' || event.value.EventTime == undefined) {
            return false;
        }
        return true;
    };

    onMounted(() => {
        fetchEvent();
    });

    const formatDate = (inputDate: string) => {
        const date = new Date(inputDate);
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const day = date.getDate().toString().padStart(2, '0');
        return `${year}-${month}-${day}`;
    };

    const toHomepage = () => {
        router.push('/');
    };

    const additionalInfoMaxlength = computed(() => 1000);
    const charactersLeft = computed(() => additionalInfoMaxlength.value - event.value.AdditionalInfo.length);
    const characterLimitExceeded = computed(() => charactersLeft.value < 0);
</script>
