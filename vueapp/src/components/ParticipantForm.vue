<template>
    <div v-if="!props.isPastEvent" class="col-md-6">
        <h3 class="mb-4">Lisa uus osaleja</h3>
        <label class="form-label">Osaleja t&uuml;&uuml;p</label><br />
        <div class="form-check form-check-inline">
            <input class="form-check-input"
                   type="radio"
                   v-model="participant.IsCompany"
                   :value="false"
                   id="false"
                   name="isCompany" />
            <label class="form-check-label" for="false">Eraisik</label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input"
                   type="radio"
                   v-model="participant.IsCompany"
                   :value="true"
                   id="true"
                   name="isCompany" />
            <label class="form-check-label" for="true">Ettev&otilde;te</label>
        </div>
        <form @submit.prevent="createParticipant">
            <div class="mb-3" v-if="!participant.IsCompany">
                <label class="form-label" for="firstName">Eesnimi</label>
                <input v-model="participant.FirstName" type="text" class="form-control" id="firstName" required />
            </div>
            <div class="mb-3" v-if="!participant.IsCompany">
                <label class="form-label" for="lastName">Perenimi</label>
                <input v-model="participant.LastName" type="text" class="form-control" id="lastName" required />
            </div>
            <div class="mb-3" v-if="participant.IsCompany">
                <label class="form-label" for="companyName">Ettev&otilde;tte nimi</label>
                <input v-model="participant.CompanyName" type="text" class="form-control" id="companyName" required />
            </div>
            <div class="mb-3">
                <label class="form-label" :for="idCodeLabel">{{ idCodeLabelText }}</label>
                <input v-model="participant.IdCode" @input="validateIdCode" type="text" class="form-control" id="idCode" required />
                <div v-if="!participant.IsCompany && !isIdCodeValid" class="text-danger">{{ message }}</div>
            </div>
            <div class="mb-3" v-if="participant.IsCompany">
                <label class="form-label" for="participantCount">Osalejate arv</label>
                <input v-model="eventParticipant.ParticipantCount" type="number" min="1" class="form-control" id="participantCount" required />
            </div>
            <div class="mb-3">
                <label class="form-label" for="paymentMethod">Maksemeetod</label><br />
                <select class="form-select form-select-lg" v-model="eventParticipant.PaymentMethod" id="paymentMethod" required>
                    <option :value="1">Panga &uuml;lekanne</option>
                    <option :value="2">Sularaha</option>
                </select>
            </div>
            <div class="mb-3">
                <label class="form-label" for="additionalInfo">Lisainfo ({{ charactersLeft }} / {{ additionalInfoMaxlength }})</label>
                <textarea v-model="eventParticipant.AdditionalInfo"
                          :maxlength="additionalInfoMaxlength"
                          class="form-control"
                          id="additionalInfo"
                          :class="{ 'is-invalid': characterLimitExceeded }"></textarea>
                <div v-if="characterLimitExceeded" class="text-danger">
                    Lisainfo &uuml;letab lubatud arvu {{ charactersLeft * -1 }} v&otilde;rra!
                </div>
            </div>
            <button type="button" @click="toHomepage()" class="btn btn-secondary" style="margin-right:3px;">Tagasi</button>
            <button type="submit" @click="submit()" :disabled="characterLimitExceeded || !isFilled()" class="btn btn-primary">Lisa uus osaleja</button>
        </form>
    </div>
</template>

<script setup lang="ts">
    import { Ref, ref, computed, defineEmits, defineProps } from 'vue';
    import type { EventParticipantDTO } from '../model/EventParticipantDTO';
    import type { ParticipantDTO } from '../model/ParticipantDTO';
    import { useRoute, useRouter } from 'vue-router';

    const props = defineProps({ isPastEvent: Boolean });

    const emit = defineEmits(['participantAdded']);
    const route = useRoute();
    const router = useRouter();
    const eventId = route.params.eventId;
    const eventParticipant: Ref<EventParticipantDTO> = ref({
        EventId: eventId.toString(),
        ParticipantId: '',
        ParticipantCount: 1,
        PaymentMethod: 0,
        AdditionalInfo: ''
    });

    const participant: Ref<ParticipantDTO> = ref({
        IdCode: '',
        FirstName: '',
        LastName: '',
        CompanyName: '',
        IsCompany: false
    });

    const isIdCodeValid = ref(true);
    const message = ref("");

    const validateIdCode = async () => {

        if (participant.value.IsCompany) {
            isIdCodeValid.value = true;
        }
        else {
            try {
                const response = await fetch('https://localhost:7165/participant/validateId/' + participant.value.IdCode, {
                    method: 'GET'
                });

                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                const data = await response.json();
                isIdCodeValid.value = data.success;
                message.value = data.message;

            } catch (error) {
                console.error('Error creating event:', error);
            }
        }
    };

    const submit = async () => {
        eventParticipant.value.ParticipantId = await getParticipantId();
        try {
            const response = await fetch('https://localhost:7165/eventParticipant', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(eventParticipant.value),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            emit('participantAdded', true);
            clearForm();
        } catch (error) {
            console.error('Error creating event:', error);
        }

    };

    const getParticipantId = async () => {
        try {
            const response = await fetch('https://localhost:7165/participant', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(participant.value),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            return data.id;
        } catch (error) {
            console.error('Error creating event:', error);
        }
    }

    const isFilled = () => {
        if (!participant.value.IsCompany) {
            if (participant.value.FirstName == '' || participant.value.LastName == '' || participant.value.IdCode.length != 11 || eventParticipant.value.PaymentMethod == 0) {
                return false;
            }
        }
        else {
            if (participant.value.CompanyName == '' || participant.value.IdCode == '' || eventParticipant.value.PaymentMethod == 0) {
                return false;
            }
        }
        return true;
    };

    const clearForm = () => {
        participant.value.IdCode = '';
        participant.value.FirstName = '';
        participant.value.LastName = '';
        participant.value.CompanyName = '';
        eventParticipant.value.ParticipantCount = 1;
        eventParticipant.value.AdditionalInfo = '';
        eventParticipant.value.PaymentMethod = 0
    };

    const toHomepage = () => {
        router.push('/');
    };

    const additionalInfoMaxlength = computed(() => (participant.value.IsCompany ? 5000 : 1500));
    const charactersLeft = computed(() => additionalInfoMaxlength.value - eventParticipant.value.AdditionalInfo.length);
    const characterLimitExceeded = computed(() => charactersLeft.value < 0);
    const idCodeLabelText = computed(() => (participant.value.IsCompany ? 'Reg. nr' : 'Isikukood'));
    const idCodeLabel = computed(() => (participant.value.IsCompany ? 'regNr' : 'idCode'));
</script>
