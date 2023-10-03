<template>
    <div>
        <div class="row">
            <div class="col-md-7">
                <h3>Tuleviku &uuml;ritused</h3>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Nimi</th>
                            <th>Kuup&auml;ev</th>
                            <th>Asukoht</th>
                            <th>Osalejate arv</th>
                            <th></th>
                  
                        </tr>
                    </thead>
                    <tbody>
                        <tr  v-for="event in events[0]" :key="event.eventId">
                            <td>{{ event.name }}</td>
                            <td>{{ formatDate(event.eventTime) }}</td>
                            <td>{{ event.location }}</td>
                            <td>{{ event.participantCount }}</td>
                            <td>
                                <button @click="toUpdateEvent(event.eventId)" class="btn btn-primary btn-sm">Muuda</button>
                                <button @click="toEventDetails(event.eventId)" class="btn btn-secondary btn-sm" style="margin-left:3px;">Osalejad</button>
                                <button @click="deleteEvent(event.eventId)" class="btn btn-danger btn-sm" style="margin-left:3px;">Kustuta</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-md-5">
                <h3>Toimunud &uuml;ritused</h3>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Nimi</th>
                            <th>Kuup&auml;ev</th>
                            <th>Asukoht</th>
                            <th>Osalejate arv</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="event in events[1]" :key="event.eventId">
                            <td>{{ event.name }}</td>
                            <td>{{ formatDate(event.eventTime) }}</td>
                            <td>{{ event.location }}</td>
                            <td>{{ event.participantCount }}</td>
                            <td><button @click="toEventDetails(event.eventId)" class="btn btn-secondary btn-sm" style="margin-left:3px;">Osalejad</button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
    import { ref, onMounted, computed } from 'vue';
    import { useRouter } from 'vue-router';

    const router = useRouter();

    const events = ref([]);

    const fetchEvents = async () => {
        try {
            const response = await fetch('https://localhost:7165/event');

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            events.value = data;
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    const toEventDetails = (eventId: String) => {
        router.push('/eventDetails/' + eventId);
    };

    const toUpdateEvent = (eventId: String) => {
        router.push('/updateEvent/' + eventId);
    };

    const deleteEvent = async (eventId: String) => {
        try {
            const response = await fetch('https://localhost:7165/event/' + eventId, {
                method: 'DELETE' 
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            fetchEvents();
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    const formatDate = (datetime: Date) => {
        const date = new Date(datetime);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}.${month}.${year}`;
    };

    onMounted(() => {
        fetchEvents();
    });
</script>
