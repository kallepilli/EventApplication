<template>
        <div class="col-md-12">
            <h2>&Uuml;rituse info</h2>
            <table class="table table-hover">
                <tbody>
                    <tr>
                        <th>&Uuml;rituse nimi:</th>
                        <td>{{ event.name }}</td>
                    </tr>
                    <tr>
                        <th>Toimumisaeg:</th>
                        <td>{{ formatDate(event.eventTime) }}</td>
                    </tr>
                    <tr>
                        <th>Koht:</th>
                        <td>{{ event.location }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

</template>

<script setup lang="ts">
    import { ref, onMounted } from 'vue';
    import { useRoute } from 'vue-router';

    const route = useRoute();
    const event = ref([]);

    const fetchEvent = async () => {
        const eventId = route.params.eventId;
        console.log(eventId);
        try {
            const response = await fetch('https://localhost:7165/event/' + eventId);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            event.value = data;
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
        fetchEvent();
    });
</script>
